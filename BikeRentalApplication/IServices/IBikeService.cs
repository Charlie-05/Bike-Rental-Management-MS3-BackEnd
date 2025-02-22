﻿using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IBikeService
    {
        Task<List<BikeResponse>> GetBike(string? type, Guid? brandId, Roles? role);
        Task<BikeResponse> GetBike(Guid id);
        Task<Bike> PutBike(BikePutRequest bikePutRequest, Guid id);
        Task<Bike> PostBike(BikeRequest bikeRequest);
        Task<MessageResponse> DeleteBike(Guid id);
    }
}
