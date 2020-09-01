using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_test.Data;
using WebAPI_test.Models;

namespace WebAPI_test.Repositories
{
    public class RepoProfiles
    {
        private static WebAPI_testContext _contextProfile;

        public RepoProfiles(WebAPI_testContext context)
        {
            _contextProfile = context;
        }
        public Profiles GetProfileByID(Guid id)
        {
            Profiles profile = _contextProfile.Profiles
                            .Where(p => p.ProfileID == id)
                            .FirstOrDefault();
            return profile;
        }

        public async Task<ActionResult<Profiles>> GetProfileByIDAsync(Guid id)
        {

            // Query for the Blog named ADO.NET Blog
            var user = GetProfileByID(id);

            return user;

        }
    }
}
