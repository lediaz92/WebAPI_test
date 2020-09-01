using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_test.Data;
using WebAPI_test.Models;

namespace WebAPI_test.Repositories
{
    public class RepoComments
    { 
        private static WebAPI_testContext _contextComment;


        public RepoComments(WebAPI_testContext context)
        {
            _contextComment = context;
        }
    }
}
