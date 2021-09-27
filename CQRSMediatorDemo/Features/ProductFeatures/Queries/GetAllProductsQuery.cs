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
    public record GetAllProductsQuery : IRequest<IEnumerable<Product>>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly DataContext context;

        public GetAllProductsQueryHandler(DataContext context) => this.context = context;
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query,
            CancellationToken cancellationToken) => await context.Products.ToListAsync();
      
    }

}
