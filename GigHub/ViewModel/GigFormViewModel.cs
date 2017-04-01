using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace GigHub.ViewModel
{
    public class GigFormViewModel
    {
        //this is done to Load the name of action Dynamically.
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]

        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }



        public DateTime getDateTime()
        {


            return DateTime.Parse(string.Format("{0} {1}", Date, Time));


        }

        public string Heading { get; set; }

        public string Action
        {


            get
            {   //this will prevent the code from being fragile.It will automaticaly update the name of action if we change the name of action in the controller

                Expression<System.Func<GigController, ActionResult>> update = (c => c.Update(this));

                Expression<System.Func<GigController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                var actionName = (action.Body as MethodCallExpression).Method.Name;
                return (actionName);


            }

        }

    }
}