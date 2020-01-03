using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Temperatures
{
    public class RetrieveList
    {
        public class Query : IRequest<List<object>> { }

        public class Handler : IRequestHandler<Query, List<object>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<object>> Handle(Query request, CancellationToken cancellationToken)
            {
                var temps = await _context.Temperatures.ToListAsync();

                var result = new List<object>(temps);

                result.AddRange(temps.Select(temp => new 
                {
                    Id = temp.Id,
                    TempFahrenheit = (temp.Temp * 9 / 5) + 32,
                    CreateDate = temp.CreateDate,
                    UpdateDate = temp.UpdateDate
                }));

                return result;
            }
        }
    }
}