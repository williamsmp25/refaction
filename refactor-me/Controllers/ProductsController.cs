using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using refactor_me.Models;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IProductRepository _repositoryProduct;
        private IProductOptionRepository _repositoryProductOption;

        public ProductsController()
            : this(new ProductRepository(), new ProductOptionRepository()) { }
        public ProductsController(IProductRepository repositoryProduct, IProductOptionRepository repositoryProductOption)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryProductOption = repositoryProductOption;
        }

        //
        // GET: /Products/
        [Route]
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            var products = _repositoryProduct.List();

            if (products.ToList().Count > 0)
                return products;
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        //
        // GET: /Products?name=<string>/
        [Route]
        [HttpGet]
        public IEnumerable<Product> SearchByName(string name)
        {
            var products = _repositoryProduct.ListByName(name);

            if (products.ToList().Count > 0)
                return products;
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        //
        // GET: /Products/{id}/
        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            Product pro = _repositoryProduct.Get(id);

            if (pro == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return pro;
        }

        //
        // POST: /Products/
        [Route]
        [HttpPost]
        public HttpResponseMessage Create(Product product)
        {
            if (product == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            Product pro = _repositoryProduct.Get(product.Id);
            if (pro != null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            _repositoryProduct.Create(product);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        //
        // PUT: /Products/{id}/
        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Update(Guid id, Product product)
        {
            if (product == null || product.Id != id)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            _repositoryProduct.Edit(product);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //
        // DELETE: /Products/{id}/
        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            Product pro = _repositoryProduct.Get(id);

            if (pro == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            _repositoryProductOption.DeleteAllProductOptions(id);
            _repositoryProduct.Delete(_repositoryProduct.Get(id));
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //
        // GET: /Products/{productId}/Options/
        [Route("{productId}/options")]
        [HttpGet]
        public IEnumerable<ProductOption> GetOptions(Guid productId)
        {
            var productOptions = _repositoryProductOption.ListAllProductOptions(productId);

            if (productOptions.ToList().Count > 0)
                return productOptions;
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        //
        // GET: /Products/{productId}/Options/{id}/
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid id)
        {
            return _repositoryProductOption.Get(id);
        }

        //
        // POST: /Products/{productId}/Options/
        [Route("{productId}/options")]
        [HttpPost]
        public HttpResponseMessage CreateOption(Guid productId, ProductOption option)
        {
            Product pro = _repositoryProduct.Get(productId);

            if (pro == null || option == null || option.ProductId != productId)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            ProductOption productOption = _repositoryProductOption.Get(option.Id);
            if (productOption != null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            _repositoryProductOption.Create(option);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        //
        // PUT: /Products/{productId}/Options/{id}/
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdateOption(Guid productId, ProductOption option, Guid id)
        {
            Product pro = _repositoryProduct.Get(productId);
            ProductOption productOption = _repositoryProductOption.Get(id);

            if (pro == null || option == null || productOption == null || option.ProductId != productId || option.Id != id)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            _repositoryProductOption.Edit(option);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //
        // DELETE: /Products/{productId}/Options/{id}/
        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteOption(Guid productId, Guid id)
        {
            Product pro = _repositoryProduct.Get(productId);
            ProductOption productOption = _repositoryProductOption.Get(id);

            if (pro == null || productOption == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            if (pro.Id != productOption.ProductId)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            _repositoryProductOption.Delete(productOption);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}
