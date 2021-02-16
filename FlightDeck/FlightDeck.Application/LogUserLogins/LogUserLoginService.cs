
using AutoMapper;
using DeviceDetectorNET;
using DeviceDetectorNET.Parser;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.Infrastructure.Service.Helper;
using FlightDeck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Application.LogUserLogins
{
    public class LogUserLoginService : ILogUserLoginService
    {
        private readonly IAsyncLogUserLoginRepository _logUserLoginRepository;
        private readonly IStringHelper _stringHelper;
        private readonly IMapper _mapper;

       
        public LogUserLoginService(IAsyncLogUserLoginRepository logUserLoginRepository, IStringHelper stringHelper, IMapper mapper)
        {
            _logUserLoginRepository = logUserLoginRepository;
            _stringHelper = stringHelper;
            _mapper = mapper;
        }
        public async Task AddUserLoginLogAsync(string userId, string userName, int attemptCount, string userAgent, string ipAddress)
        {

            var modelVM = new LogUserLoginVM();

            #region device detection section

            DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_NONE);
            var dd = new DeviceDetector(userAgent);
            dd.Parse();
            var clientInfo = dd.GetClient(); 
            var osInfo = dd.GetOs();
            var device = dd.GetDeviceName();
            var deviceBrand = dd.GetBrandName();
            var deviceModel = dd.GetModel();

            #endregion

            modelVM.ClientInfo = _stringHelper.RemoveSpaceFromString(clientInfo.Match.ToString());
            modelVM.OSInfo = _stringHelper.RemoveSpaceFromString(osInfo.Match.ToString());
            modelVM.Device = device;
            modelVM.DeviceBrand = deviceBrand;
            modelVM.DeviceModel = deviceModel;
            modelVM.UserId = userId;
            modelVM.UserName = userName;
            modelVM.LoginAttemptCount = attemptCount;
            modelVM.IpAddress = ipAddress;
            modelVM.LoginAttemptDate = DateTime.Now;

            var model = _mapper.Map<LogUserLogin>(modelVM);
            await _logUserLoginRepository.AddAsync(model);

        }
    }
}
