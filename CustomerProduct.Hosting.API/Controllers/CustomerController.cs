using CustomerProduct.Business.Contracts;
using CustomerProduct.Common.EntityResponseStructure;
using CustomerProduct.Common.Enums;
using CustomerProduct.Data.Entities;
using CustomerProduct.Hosting.API.Common;
using CustomerProduct.Hosting.API.Models.RequestModels;
using CustomerProduct.Hosting.API.Models.ResponseModels;
using CustomerProduct.Hosting.API.Validator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static CustomerProduct.Common.Enums.Enums;

namespace CustomerProduct.Hosting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerProductRepository _customerProductRepository;
        private readonly IProductRepository _productRepository;

        public CustomerController(ICustomerRepository customerRepository, ICustomerProductRepository customerProductRepository, IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _customerProductRepository = customerProductRepository;
            _productRepository = productRepository;
        }


        [HttpPost]
        [Route("selectproduct")]
        public ActionResult<ApiResponse<CustomerProductResponse>> Post([FromBody] CustomerProductCreateRequest request)
        {
            ServicePrimitiveResponse insertCustomerProductResult = new ServicePrimitiveResponse();

            //Fluent Validation Controls
            CustomerProductValidator validator = new CustomerProductValidator();
            ValidationResult results = validator.Validate(request);

            if (results.IsValid)
            {
                //Check IdentificationNo IsExist
                bool isExistCustomer = _customerRepository.FindFirstBy(p => p.IdentificationNumber.Equals(request.IdentificationNumber)).ResponseCode == (int)EntityResponseCodes.NoRecordFound;
                if (isExistCustomer)
                    return BadRequest(new ApiResponse { Code = (int)ApiResponseCodes.ValidationError, ErrorMessage = " Identification Number Is Not Exist!" });

                //Check ProductId IsExist
                bool isExistProduct = _productRepository.Find(request.ProductId).ResponseCode == (int)EntityResponseCodes.NoRecordFound;
                if (isExistProduct)
                    return BadRequest(new ApiResponse { Code = (int)ApiResponseCodes.ValidationError, ErrorMessage = "Product Id Is Not Exist!" });

                insertCustomerProductResult = _customerProductRepository.InsertCustomerProduct(request.IdentificationNumber, request.ProductId);

                if (insertCustomerProductResult.ResponseCode == (int)EntityResponseCodes.Successfull)
                    return Get(request.IdentificationNumber); //If Success Call Get ProductInfo endpoint
            }
            else
                return BadRequest(GetValidationErrors(results));

            throw new ApiException((int)ApiResponseCodes.DbError, insertCustomerProductResult.ResponseMessage);

        }


        public ApiResponse GetValidationErrors(ValidationResult results)
        {
            //Handle Validation Errors
            List<string> validationErrorList = new List<string>();

            foreach (var value in results.Errors)
            {
                validationErrorList.Add(value.ErrorMessage);
            }

            var response = new ApiResponse
            {
                Code = (int)ApiResponseCodes.ValidationError,
                ValidationErrors = validationErrorList
            };

            return response;
        }

        [HttpGet]
        [Route("selectedproducts")]
        public ActionResult<ApiResponse<CustomerProductResponse>> Get([FromQuery] string identificationNumber)
        {
            //Check identificationNumber Field
            if (string.IsNullOrEmpty(identificationNumber))
                return BadRequest(new ApiResponse { Code = (int)ApiResponseCodes.ValidationError, ErrorMessage = "IdentificationNumber Required" });

            //Check IdentificationNo IsExist
            bool isExistCustomer = _customerRepository.FindFirstBy(p => p.IdentificationNumber.Equals(identificationNumber)).ResponseCode == (int)EntityResponseCodes.NoRecordFound;
            if (isExistCustomer)
                return BadRequest(new ApiResponse { Code = (int)ApiResponseCodes.ValidationError, ErrorMessage = " Identification Number Is Not Exist!" });

            var serviceEntityProductResponse  = _customerProductRepository.GetSelectedProductsByIdentificationNumber(identificationNumber);

            if(serviceEntityProductResponse.ResponseCode == (int)Enums.EntityResponseCodes.Successfull)
            {
                return Ok(new ApiResponse<IEnumerable<CustomerProductResponse>>((int)ApiResponseCodes.Ok, serviceEntityProductResponse.EntityDataList.Select(x => new CustomerProductResponse
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    Description = x.Product.Description,
                    Price = x.Product.Price
                })));
            }

            throw new ApiException((int)ApiResponseCodes.DbError, serviceEntityProductResponse.ResponseMessage);
        }


        [HttpDelete]
        [Route("unselectproduct")]
        public ActionResult<ApiResponse<int>> Delete([FromBody] CustomerProductDeleteRequest request)
        {
            ServicePrimitiveResponse deleteCustomerProductResponse = new ServicePrimitiveResponse();

            //Fluent Validation Controls
            CustomerProductValidator validator = new CustomerProductValidator();
            ValidationResult results = validator.Validate(request);

            if (results.IsValid)
            {
                //Check IdentificationNo IsExist
                bool isExistCustomer = _customerRepository.FindFirstBy(p => p.IdentificationNumber.Equals(request.IdentificationNumber)).ResponseCode == (int)EntityResponseCodes.NoRecordFound;
                if (isExistCustomer)
                    return BadRequest(new ApiResponse { Code = (int)ApiResponseCodes.ValidationError, ErrorMessage = " Identification Number Is Not Exist!" });

                //Check ProductId IsExist
                bool isExistProduct = _productRepository.Find(request.ProductId).ResponseCode == (int)EntityResponseCodes.NoRecordFound;
                if (isExistProduct)
                    return BadRequest(new ApiResponse { Code = (int)ApiResponseCodes.ValidationError, ErrorMessage = "Product Id Is Not Exist!" });


                //Get CustomerProductInfo
                ServiceEntityResponse<Data.Entities.CustomerProduct> serviceEntityResponse = _customerProductRepository.FindFirstBy(p => p.IdentificationNumber.Equals(request.IdentificationNumber) && p.ProductId == request.ProductId);

                if (serviceEntityResponse.ResponseCode == (int)Enums.EntityResponseCodes.Successfull)
                {
                    _customerProductRepository.Delete(serviceEntityResponse.EntityData);
                    
                    deleteCustomerProductResponse = _customerProductRepository.Save();
                    
                    if(deleteCustomerProductResponse.ResponseCode == (int)EntityResponseCodes.Successfull)
                        return Ok(new ApiResponse { Code = (int)ApiResponseCodes.Ok });
                    else
                        throw new ApiException((int)ApiResponseCodes.DbError, deleteCustomerProductResponse.ResponseMessage);
                }
            }
               
            return BadRequest(GetValidationErrors(results));

        }





    }
}
