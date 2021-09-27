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
    public record CreateProductCommand(Product product) : IRequest<Product>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly DataContext context;
        public CreateProductCommandHandler(DataContext context) => this.context = context;
        public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
     
            context.Products.Add(command.product);

            await context.SaveChangesAsync();

            return command.product;
        }
    }
}
