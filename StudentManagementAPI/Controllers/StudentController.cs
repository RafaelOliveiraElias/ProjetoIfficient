using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Core.Interfaces;
using StudentManagementAPI.Services.enums;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _service;

    public StudentController(StudentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna todos os alunos.
    /// </summary>
    /// <returns>Lista de alunos.</returns>
    [HttpGet("all")]
    [SwaggerOperation(Summary = "Retorna todos os alunos cadastrados.")]
    public IActionResult GetAll() => Ok(_service.GetAllStudents());

    /// <summary>
    /// Retorna todos os alunos aprovados.
    /// </summary>
    /// <returns>Lista de alunos aprovados.</returns>
    [HttpGet("approved")]
    [SwaggerOperation(Summary = "Retorna todos os alunos aprovados.")]
    public IActionResult GetApproved() => Ok(_service.GetApprovedStudents());

    /// <summary>
    /// Retorna todos os alunos reprovados.
    /// </summary>
    /// <returns>Lista de alunos reprovados.</returns>
    [HttpGet("disapproved")]
    [SwaggerOperation(Summary = "Retorna todos os alunos reprovados.")]
    public IActionResult GetDisapproved() => Ok(_service.GetDisapprovedStudents());

    /// <summary>
    /// Retorna os dados de um aluno específico baseado na matrícula.
    /// </summary>
    /// <param name="registration">Número da matrícula do aluno.</param>
    /// <returns>Dados do aluno com a matrícula fornecida.</returns>
    [HttpGet("{registration}")]
    [SwaggerOperation(Summary = "Retorna os dados de um aluno específico pela matrícula.")]
    public IActionResult GetByRegistration(string registration)
    {
        var student = _service.GetStudentByRegistration(registration);

        if (student is null)
        {
            return NotFound("Não existe aluno com essa matrícula.");
        }

        return Ok(student);
    }

    /// <summary>
    /// Retorna o melhor aluno por matéria (maior nota).
    /// </summary>
    /// <returns>Lista com a matéria, nome do melhor aluno e sua nota.</returns>
    [HttpGet("best-by-subject")]
    [SwaggerOperation(Summary = "Retorna o melhor aluno por matéria.")]
    public IActionResult GetBestStudentsBySubject()
    {
        var bestStudents = _service.GetBestStudentBySubject();

        var result = bestStudents.Select(kvp => new
        {
            Subject = kvp.Key,
            BestStudent = new
            {
                kvp.Value.BestStudent.Name,
                kvp.Value.BestStudent.Registration
            },
            Grade = kvp.Value.Grade
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para retornar os alunos ordenados com base na estratégia escolhida.
    /// O usuário pode escolher a estratégia de ordenação desejada.
    /// </summary>
    /// <param name="strategy">Escolha a estratégia de ordenação. As opções são "Bubble Sort (0)" ou "LINQ Sort (1)".</param>
    /// <returns>Lista de alunos ordenados com base na estratégia escolhida.</returns>
    [HttpGet("sorted")]
    [SwaggerOperation(Summary = "Retorna alunos ordenados de acordo com a estratégia escolhida.")]
    public IActionResult GetSortedStudents([FromQuery] SortStrategyType strategy)
    {
        try
        {
            var sortedStudents = _service.GetSortedStudentsByAverage(strategy);
            return Ok(sortedStudents);
        }
        catch (ArgumentException)
        {
            return BadRequest("Invalid sorting strategy provided.");
        }
    }
}
