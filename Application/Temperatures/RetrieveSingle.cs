using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Temperatures
{
    public class RetrieveSingle
    {
        public class Query : IRequest<Temperature> 
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Temperature>
        {
            private readonly DataContext _context;
            
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Temperature> Handle(Query request, CancellationToken cancellationToken)
            {
                var temp = await _context.Temperatures.SingleOrDefaultAsync(x => x.Id == request.Id);

                return temp;
            }
        }
    }
}