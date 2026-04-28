using Microsoft.AspNetCore.Mvc;
using RemittanceTest.Models;
using RemittanceTest.Services;

namespace RemittanceTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemittanceController : ControllerBase
    {
        // TODO: 1. 請透過建構子注入 (Constructor Injection) 引入 IRemittanceService
        private readonly IRemittanceService _remittanceService;
        public RemittanceController(IRemittanceService remittanceService) {
            _remittanceService = remittanceService;
        }

        [HttpPost("{id}/cancel")]
        public IActionResult Cancel(int id)
        {
            // TODO: 2. 呼叫 Service 執行取消邏輯
            var result = _remittanceService.CancelRemittance(id);

            // TODO: 3. 根據 Service 回傳的結果，回傳相對應的 HTTP 狀態碼 (Ok / BadRequest / NotFound)
            if (result.IsSuccess)
            {
                return Ok(new
                {
                    isSuccess = result.IsSuccess,
                    message = result.Message,
                });
            }
            
            if (result.Message == "資料不存在")
            {
                return NotFound(new
                {
                    isSuccess = result.IsSuccess,
                    message = result.Message,
                });
            }

            return BadRequest(new
            {
                isSuccess = result.IsSuccess,
                message = result.Message,
            });
        }
    }
}