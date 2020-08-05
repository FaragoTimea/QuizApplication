using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Data;
using QuizApplication.DTO;
using QuizApplication.Interfaces;

namespace QuizApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private QuizDbContext _context;
        private IMapper _mapper;
        private IQuizChecker _quizChecker;

        public QuizController(
            QuizDbContext context,
            IMapper mapper,
            IQuizChecker quizChecker)
        {
            _context = context;
            _mapper = mapper;
            _quizChecker = quizChecker;
        }


        //GET quiz with questions but no correct answers
        [HttpGet("{quizId}")]
        public async Task<ActionResult<QuizDTO>> GetQuizAsync(int quizId)
        {
            var quiz = await _context.Quizes.FindAsync(quizId);
            if (quiz is null) return BadRequest("Quiz doesnt exist");


            var questions = await _context.Questions
                .Include(q => q.AnswerOptions)
                .Where(q => q.Quiz == quiz)
                .ToListAsync();
            var qs = _mapper.Map<List<QuestionDTO>>(questions);

            foreach (var item in qs)
            {
                //change every option to not correct in the DTO
                foreach (var opt in item.AnswerOptions)
                {
                    opt.IsCorrect = false;
                }
            }
            var result = _mapper.Map<QuizDTO>(quiz);
            result.Questions = qs;
            return result;
        }

        

        //POST quiz guesses
        [HttpPost("{quizId}")]
        public async Task<ActionResult<ResultDTO>> PostQuiz(int quizId, [FromBody] GuessDTO dto)
        {
            var quiz = await _context.Quizes.FindAsync(quizId);
            if (quiz is null) return BadRequest("Quiz doesnt exist");

            var qs = await _context.Questions.Where(q => q.Quiz == quiz)
                .Include(q => q.AnswerOptions).ToListAsync();
            var result = _quizChecker.CheckQuiz(qs, dto);
            result.QuizId = quizId;

            return result;
        }
    }
}
