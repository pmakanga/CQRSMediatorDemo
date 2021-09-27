using CQRSMediatorDemo.Context;
using CQRSMediatorDemo.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatorDemo.Features.ProductFeatures.Commands
{
    public record UpdateProductCommand(Product product) : IRequest<int>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly DataContext context;
        public UpdateProductCommandHandler(DataContext context) => this.context = context;
        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = context.Products.Where(a => a.Id == command.product.Id).FirstOrDefault();
            if (product == null) return default;

            product.Barcode = command.product.Barcode;
            product.Name = command.product.Name;
            product.BuyingPrice = command.product.BuyingPrice;
            product.Rate = command.product.Rate;
            product.Description = command.product.Description;
            await context.SaveChangesAsync();
            return product.Id;
        }
    }
    
}
