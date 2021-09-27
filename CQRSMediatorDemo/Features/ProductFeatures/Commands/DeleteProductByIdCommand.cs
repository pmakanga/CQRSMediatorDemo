using CQRSMediatorDemo.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatorDemo.Features.ProductFeatures.Commands
{
    public record DeleteProductByIdCommand(int Id) : IRequest<int>;

    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, int>
    {
        private readonly DataContext context;
        public DeleteProductByIdCommandHandler(DataContext context) => this.context = context;
        
        public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = await context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
            if (product == null) return default;
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return product.Id;
        }

       
    }


}
