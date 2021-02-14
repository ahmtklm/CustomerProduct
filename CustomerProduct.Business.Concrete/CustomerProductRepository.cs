using CustomerProduct.Business.Contracts;
using CustomerProduct.Common;
using CustomerProduct.Common.EntityResponseStructure;
using CustomerProduct.Common.Enums;
using CustomerProduct.Data;
using CustomerProduct.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CustomerProduct.Business.Concrete
{
    public class CustomerProductRepository : GenericRepository<Data.Entities.CustomerProduct>, ICustomerProductRepository
    {
        public CustomerProductRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public ServiceEntityResponse<CustomerProductModel> GetSelectedProductsByIdentificationNumber(string identificationNumber)
        {
            ServiceEntityResponse<CustomerProductModel> serviceEntityResponse = new ServiceEntityResponse<CustomerProductModel>();

            try
            {
                using (CustomerProductContext container = (Context as CustomerProductContext))
                {
                    var customerProducts = (from cp in container.CustomerProduct
                                            join p in container.Products on cp.ProductId equals p.ProductId
                                            join cu in container.Customers on cp.IdentificationNumber equals cu.IdentificationNumber
                                            where cp.IdentificationNumber.Equals(identificationNumber)
                                            select new CustomerProductModel
                                            {
                                                Customer = cu,
                                                Product = p
                                            }).ToList();

                    if (customerProducts.Any())
                    {
                        serviceEntityResponse.ResponseCode = (int)Enums.EntityResponseCodes.Successfull;
                        serviceEntityResponse.EntityDataList = customerProducts;
                    }
                    else
                        serviceEntityResponse.ResponseCode = (int)Enums.EntityResponseCodes.NoRecordFound;
                }
            }
            catch (Exception ex)
            {
                serviceEntityResponse.ResponseCode = (int)Enums.EntityResponseCodes.DbError;
                serviceEntityResponse.ResponseMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return serviceEntityResponse;

        }

        public ServicePrimitiveResponse InsertCustomerProduct(string identificationNumber, int productId)
        {
            ServicePrimitiveResponse primitiveResponse = new ServicePrimitiveResponse();

            CustomerProductContext context = (Context as CustomerProductContext);

            try
            {
                if (context != null)
                {
                    Data.Entities.CustomerProduct customerProduct = new Data.Entities.CustomerProduct
                    {
                        IdentificationNumber = identificationNumber,
                        ProductId = productId
                    };

                    context.CustomerProduct.Add(customerProduct);
                    context.SaveChanges();
                    primitiveResponse.ResponseCode = (int)Enums.EntityResponseCodes.Successfull;

                }
                else
                    primitiveResponse.ResponseCode = (int)Enums.EntityResponseCodes.DbError;
            }
            catch (Exception ex)
            {
                primitiveResponse.ResponseCode = (int)Enums.EntityResponseCodes.DbError;
                primitiveResponse.InnerException = ex.InnerException;
                primitiveResponse.ResponseMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

            }



            return primitiveResponse;

        }

    }
}
