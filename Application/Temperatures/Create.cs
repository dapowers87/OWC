using MediatR;
using Persistence;
using Domain;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Temperatures
{
    public class Create
    {
        public class Command : IRequest
        {
            public decimal Temp { get; set; }
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
                var temperature = new Temperature
                {
                    CreateDate = DateTime.Now,
                    Temp = request.Temp
                };

                await _context.Temperatures.AddAsync(temperature);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem saving changes");
            }
        }
    }
}