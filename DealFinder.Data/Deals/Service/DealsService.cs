﻿using System.Linq;
using DealFinder.Data.Deals.Repository;

namespace DealFinder.Data.Deals.Service
{
    public interface IDealsService
    {
        GetDealsByLocationResponse GetByLocation(double latitude, double longitude, string userIdentifier);
        SaveDealDetailsResponse SaveDealDetails(DealModel deal);
    }

    public class DealsService : IDealsService
    {
        private readonly IDealsRepository _dealsRepository;

        public DealsService(IDealsRepository dealsRepository)
        {
            _dealsRepository = dealsRepository;
        }

        public GetDealsByLocationResponse GetByLocation(double latitude, double longitude, string userIdentifier)
        {
            var response = new GetDealsByLocationResponse();

            var getDealsByLocationResponse = _dealsRepository.GetByLocation(latitude, longitude);

            if (getDealsByLocationResponse.HasError)
            {
                response.AddError(getDealsByLocationResponse.Error);
                return response;
            }

            response.Deals = DealsMapper.Map(getDealsByLocationResponse.Deals, userIdentifier).OrderBy(x => x.DistanceInMeters).ToList();
            return response;
        }

        public SaveDealDetailsResponse SaveDealDetails(DealModel deal)
        {
            var response = new SaveDealDetailsResponse();

            var saveDealDetailsResponse = _dealsRepository.SaveDeal(new SaveDealRequest
            {
                Deal = deal
            });

            if (saveDealDetailsResponse.HasError)
                response.AddError(saveDealDetailsResponse.Error);

            return response;
        }
    }
}