﻿using Azure.Core;
using Newtonsoft.Json;
using School.Models.DTO;
using School.Models.DTO.IdentityDTO;
using System.Text;

namespace School.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private IHttpClientFactory _httpClientFactory;
        public IdentityService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var jsonContent = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient("Identity");
            var response = await client.PostAsync($"/api/identity/login", stringContent);
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(resp.Result));
            }
            return new LoginResponseDTO();
        }

        public async Task<StudentDto> Register(RegistrationRequestDTO model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient("Identity");
            var response = await client.PostAsync($"/api/identity/register", stringContent);
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<StudentDto>(Convert.ToString(resp.Result));
            }
            return new StudentDto();
        }
    }
}
