
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {

        private readonly IChatCompletionService _chatService;
        private readonly ChatHistory _chatMessages;
        public ChatController(IChatCompletionService service)
        {
            _chatService = service;
            _chatMessages =
            [
                new()
                {
                    Role = AuthorRole.System,
                    Content = "You are a helpful Banking Assistant who helps customers with their queries. Provide relevant answer and be polite. Answer all the customer's queries. You are also a chatbot who answers FAQs "
                }
,
            ];
        }

        [HttpPost]
        public async Task<ActionResult<ChatMessageContent>> Chat([FromBody] Question question)
        {

            _chatMessages.AddUserMessage(question.UserQuestion);
            var result = await _chatService.GetChatMessageContentAsync(_chatMessages);
            
            return Ok(result.Content);
        }


    }
}