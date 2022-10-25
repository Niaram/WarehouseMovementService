using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseMovementService.Application.ReadModel;

namespace WarehouseMovementService.Application.MediatorRequestHandlers
{
    public class GetWarehouseAvailabilitiesQuery : IRequest<List<GetWarehouseAvailabilitiesModel>>
    {
    }

    public class GetWarehouseAvailabilitiesQueryHandler : IRequestHandler<GetWarehouseAvailabilitiesQuery, List<GetWarehouseAvailabilitiesModel>>
    {
        private readonly IReadWarehouseAvailabilityRepository readWarehouseAvailabilityRepository;

        public GetWarehouseAvailabilitiesQueryHandler(IReadWarehouseAvailabilityRepository readWarehouseAvailabilityRepository)
        {
            this.readWarehouseAvailabilityRepository = readWarehouseAvailabilityRepository;
        }

        /*
         * NOTE
         * in this case we should use the read model.
         * ReadModel can be implemented with a dedicated way (different DB, different ORM, ...)
         * i like to use Dapper in this case but there is no need to do it because we have this API just to make the application usable by users 
         * (this API will be not called by any frontend or any other service).
         * I will use EFCore also here in order to simplify the code.
         */
        public async Task<List<GetWarehouseAvailabilitiesModel>> Handle(GetWarehouseAvailabilitiesQuery request, CancellationToken cancellationToken)
            => await readWarehouseAvailabilityRepository.GetWarehouseAvailabilities();

    }
}
