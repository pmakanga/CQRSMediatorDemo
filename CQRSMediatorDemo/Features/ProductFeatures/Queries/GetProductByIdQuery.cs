using CQRSMediatorDemo.Context;
using CQRSMediatorDemo.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatorDemo.Features.ProductFeatures.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly DataContext context;
        public GetProductByIdQueryHandler(DataContext context) => this.context = context;
        public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await context.Products.Where(a => a.Id == query.Id).FirstOrDefaultAsync();

            if (product == null) return null;

            return product;
        }
    }
}
