using CustomerProduct.Business.Contracts;
using CustomerProduct.Common.EntityResponseStructure;
using CustomerProduct.Common.Enums;
using CustomerProduct.Data.Entities;
using CustomerProduct.Hosting.API.Common;
using CustomerProduct.Hosting.API.Models.RequestModels;
using CustomerProduct.Hosting.API.Models.ResponseModels;
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
            var result = _customerProductRepository.InsertCustomerProduct(request.IdentificationNumber, request.ProductId);

            if(result.ResponseCode == (int)EntityResponseCodes.Successfull)
            {
                //return Ok(new ApiResponse { Code = (int)ApiResponseCodes.Ok });
                return Get(request.IdentificationNumber);
            }

            return Ok(new ApiResponse { Code = (int)ApiResponseCodes.Ok });

        }

        [HttpGet]
        [Route("{identificationNumber}/selectedproducts")]
        public ActionResult<ApiResponse<CustomerProductResponse>> Get(string identificationNumber)
        {
            //Get Products For Customer Identification Number

            var serviceEntityResponse = _customerProductRepository.GetSelectedProductsByIdentificationNumber(identificationNumber);

            if(serviceEntityResponse.ResponseCode == (int)Enums.EntityResponseCodes.Successfull)
            {
                return Ok(new ApiResponse<IEnumerable<CustomerProductResponse>>((int)ApiResponseCodes.Ok, serviceEntityResponse.EntityDataList.Select(x => new CustomerProductResponse
                {
                    ProductId = x.Product.ProductId,
                    ProductName = x.Product.ProductName,
                    Description = x.Product.Description,
                    Price = x.Product.Price
                })));
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("unselectproduct")]
        public ActionResult<ApiResponse<int>> Delete([FromBody] CustomerProductDeleteRequest request)
        {
            ServiceEntityResponse<Data.Entities.CustomerProduct> serviceEntityResponse = _customerProductRepository.FindFirstBy(p => p.IdentificationNumber.Equals(request.IdentificationNumber) && p.ProductId == request.ProductId);

            if(serviceEntityResponse.ResponseCode == (int)Enums.EntityResponseCodes.Successfull)
            {
                _customerProductRepository.Delete(serviceEntityResponse.EntityData);
                _customerProductRepository.Save();

                return Ok(new ApiResponse { Code = (int)ApiResponseCodes.Ok });
            }

            return Ok(new ApiResponse<int>((int)ApiResponseCodes.Ok, Convert.ToInt32(1)));
        }





    }
}
