﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRChat.Mobile.iOS.Services;
using SignalRChat.Mobile.Services;
using Foundation;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authenticator))]
namespace SignalRChat.Mobile.iOS.Services
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }
    }

}