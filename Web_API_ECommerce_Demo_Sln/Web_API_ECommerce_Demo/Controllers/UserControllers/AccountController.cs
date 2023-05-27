using AutoMapper;
using ECommerce_Demo_Core.Entities.Identity;
using ECommerce_Demo_Core.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_API_ECommerce_Demo.Dtos.AuthDtos;
using Web_API_ECommerce_Demo.Errors;
using Web_API_ECommerce_Demo.Extentions;

namespace Web_API_ECommerce_Demo.Controllers.UserControllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(AppUserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new AppUserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user,_userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserDto>> Register(AppUserRegisterDto model)
        {
            if (CheckEmail(model.Email).Result.Value) return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This mail is already in use" } });

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.PassWord);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new AppUserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new AppUserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var address = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            var address = _mapper.Map<AddressDto,Address>(updatedAddress);
            var user = await _userManager.FindUserWithAddressAsync(User);

            address.Id = user.Address.Id;
            user.Address = address;

            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(updatedAddress);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null; 
        }
    }
}
