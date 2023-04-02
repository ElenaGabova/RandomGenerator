using AutoMapper;
using Domain;
using Entities;
using Inreface;
using Interface;
using Microsoft.AspNetCore.Mvc;
using static Constants.Constants;

namespace RandomNumbersProject.Controllers
{

    [Route("api/[controller]")]
    public class NumbersController : Controller
    {
        IRandomGeneratorRepository _randomGeneratorRepository;
        INumberRepository _numberRepository;
        IRandomCounterRepository _randomCounterRepository;
        INumberRepetitionRepository _numberRepetitionRepository;
        IMapper _mapper;

        public NumbersController(IRandomGeneratorRepository randomGeneratorRepository,
                                 IRandomCounterRepository randomCounterRepository,
                                 INumberRepetitionRepository numberRepetitionRepository,
                                 INumberRepository numberRepository,
                                 IMapper mapper)
        {
            _randomGeneratorRepository = randomGeneratorRepository;
            _numberRepository          = numberRepository;
            _randomCounterRepository   = randomCounterRepository;
            _numberRepetitionRepository = numberRepetitionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Генерация последовательности чисел
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /AddGenerateNumbers
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpPost("AddGenerateNumbers")]
        public async Task<IActionResult> AddGenerateNumbers()
        {
           var numbersList = await _randomGeneratorRepository.GenerateNumbers(MinNumber, MaxNumber, NumbersCount);
           var mappedNumbersList = numbersList.Select(n => new Number(n)).ToList();

           await _numberRepository.AddRangeAsync(mappedNumbersList);
           return Ok();
        }

        /// Подсчет количества чисел
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /AddCountNumbers
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpPost("AddCountNumbers")]
        public async Task<IActionResult> AddCountNumbers()
        {
            var numbersFromDatabase = await _numberRepository.GetAsync();
            var mappedNumbersFromDatabaseArray = numbersFromDatabase.Select(n => n.NumberValue).ToList();

            var numbersCountDictonary = _randomCounterRepository.GetCountNumbers(mappedNumbersFromDatabaseArray);
            var numbersCountToDatabase = numbersCountDictonary.Select(n => new NumberRepetition(n.Key, n.Value)).ToList();
            await _numberRepetitionRepository.AddRangeAsync(numbersCountToDatabase);
            return Ok();
        }

        /// Подсчет количества повторений числа в последовательности
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /GetNumberByNumberRepetitions
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="number">Число</param>
        /// <returns>Количество повторений чисел</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("GetNumberRepetitionByNumber")]
        public async Task<IActionResult> GetNumberRepetitionByNumber(int number)
        {
            int result = await _numberRepetitionRepository.GetNumberRepetitionByNumber(number);
            return Ok(result);
        }

        /// Подсчет количества чисел, с заданным количеством повторений последовательности
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /GetNumberByNumberRepetitions
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="repetition">Количество повторений чисел</param>
        /// <returns>Числа, с таким количеством повторений</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("GetNumberByNumberRepetitions")]
        public async Task<IActionResult> GetNumberByNumberRepetitions(int repetition)
        {
            List<int> result = await _numberRepetitionRepository.GetNumberByNumberRepetitions(repetition);
            return Ok(result);
        }

        /// Вывод топ чисел с самым большим количеством повторений
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /GetNumbersWithTopRepetitions
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="topCount">Количество чисел, которое нужно вывести</param>
        /// <returns>Числа  с самым большим количеством повторений</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("GetNumbersWithTopRepetitions")]
        public async Task<IActionResult> GetNumbersWithTopRepetitions(int topCount = 10)
        {
            List<int> result = await _numberRepetitionRepository.GetNumbersWithTopRepetitions(topCount);
            return Ok(result);
        }
    }
}
