﻿using AuthServer.Models;
using AuthServer.Models.Auth;
using AuthServer.Models.Responce;
using AuthServer.Services.RefreshTokenRepositories;
using AuthServer.Services.TokenGenerators;

namespace AuthServer.Services.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public Authenticator(
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticatedUserResponce> Authenticate(User user)
        {
            string accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new()
            {
                Token = refreshToken,
                UserId = user.Id,
            };
            await _refreshTokenRepository.Create(refreshTokenDTO);

            return new AuthenticatedUserResponce()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
