using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Helpers.Exceptions;
using Newtonsoft.Json;

namespace Helpers
{
    internal static class GameVersionChecker
    {
        private const string VersionEndPoint = "https://global.api.pvp.net/api/lol/static-data/euw/v1.2/versions?api_key=ee57feb3-1da3-441d-807a-7486e0559e72";
        private const int SyncrhonizationTimeOutInHours = 24;

        private static string _gameVersion;
        private static DateTime _timeStamp;

        static GameVersionChecker()
        {
            SynchronizeGameVersion();
        }

        private static string GameVersion
        {
            get
            {
                return _gameVersion;
            }
            set
            {
                if (value.IsInvalid())
                {
                    throw new ArgumentException("Game version must not be empty, null or plain whitespaces. ");
                }

                _gameVersion = value;
            }
        }

        internal static string GetGameVersion()
        {
            var currentTimeStamp = DateTime.Now;
            var timeStampOffset = _timeStamp.AddHours(SyncrhonizationTimeOutInHours);
            if (currentTimeStamp >= timeStampOffset)
            {
                SynchronizeGameVersion();
            }

            return GameVersion;
        }

        private static void SynchronizeGameVersion()
        {
            var httpClient = new HttpClient();
            var gameVersionResponse = httpClient.GetAsync(VersionEndPoint).Result;
            if (!gameVersionResponse.IsSuccessStatusCode)
            {
                throw new FailedOperationException(string.Format("Could not get game version with server response code: {0}", gameVersionResponse.StatusCode));
            }

            var gameVersionsJson = gameVersionResponse.Content.ReadAsStringAsync().Result;
            var gameVersionObject =
                JsonConvert.DeserializeObject<string[]>(gameVersionsJson);

            GameVersion = gameVersionObject[0];
            _timeStamp = DateTime.Now;
        }
    }
}
