using SaGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaGService.Controllers
{
    //https://blog.darkthread.net/blog/postbody-param-to-webapi2/
    public class SampleController : ApiController
    {
        Sample[] samples = new Sample[] {
            new Sample { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Sample { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Sample { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        //Get api/samples/
        public IEnumerable<Sample> GetAll()
        {
            return samples;
        }

        //Get api/samples/id
        public IHttpActionResult Get(int id)
        {
            var sample = samples.FirstOrDefault((p) => p.Id == id);
            if (sample == null)
            {
                return NotFound();
            }
            return Ok(sample);
        }

        //GET api/samples/?name=xxx
        public IEnumerable<Sample> GetByName(string name)
        {
            return samples.Where(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        //POST api/samples Entity Json
        public IHttpActionResult Post([FromBody]Sample sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          //  db.Samples.Add(sample);

            try
            {
               // db.SaveChanges();
            }
            catch (Exception ex)
            {
                //if (SampleExists(sample.Id))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return CreatedAtRoute("GrossApi", new { id = sample.Id }, sample);
        }

        // PUT api/samples/id
        public IHttpActionResult Put(int id, [FromBody]Sample sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sample.Id)
            {
                return BadRequest();
            }

            //db.Entry(sample).State = EntityState.Modified;

            try
            {
                //db.SaveChanges();
            }
            catch (Exception ex)
            {
            //    if (!SampleExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            }
            //return StatusCode(HttpStatusCode.Unauthorized);
            return Ok(sample);
        }

        //DELETE api/samples/id
        public IHttpActionResult Delete(int id)
        {
            //Sample sample = samples.Find(id);
            //if (sample == null)
            //{
            //    return NotFound();
            //}

            //db.Samples.Remove(sample);
            //db.SaveChanges();

            //return Ok(sample);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
       //         db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SampleExists(string id)
        {
            // return db.Customers.Count(e => e.CustomerID == id) > 0;
            return true;
        }
    }
}
