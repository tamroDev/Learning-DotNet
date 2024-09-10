using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// Sử dụng từ khóa using MyWebApi.Models để nói với ASP . net Core biết tôi muốn sử dụng các lớp từ không gian này
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {

        public static List<HangHoa> hangHoas = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id) 
        {   
            try
            {
                //LINQ [Object] Query
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null) { 
                    return NotFound();
                }

                return Ok(hangHoa);

            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateNew(HangHoaVM hangHoaVM)
        {
            var hanghoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };

            hangHoas.Add(hanghoa);

            return Ok(new
            {
                Succces = true, Data = hanghoa
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit ( string id, HangHoa hangHoaEdit)
        {
            try
            {
                //LINQ [Object] Query
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }

                if(id != hangHoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                } 

                hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                hangHoa.DonGia = hangHoaEdit.DonGia;

                return Ok(); 
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                //LINQ [Object] Query
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }

                if (id != hangHoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                // Xóa hàng hóa
                hangHoas.Remove(hangHoa);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
