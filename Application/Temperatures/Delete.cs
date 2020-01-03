using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Temperatures
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; } 
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var temp = await _context.Temperatures.SingleOrDefaultAsync(x => x.Id == request.Id);

                _context.Temperatures.Remove(temp);
                
                var success = await _context.SaveChangesAsync() > 0;;

                if(success)
                {
                    return Unit.Value;
                }

                throw new Exception("Error deleting temperature");
            }
        }
    }
}