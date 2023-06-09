﻿using LABMedicine.Context;
using LABMedicine.DTO;
using LABMedicine.Enumerator;
using LABMedicine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LABMedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentosController : ControllerBase
    {
        private readonly labmedicinebdContext labmedicinebd;

        // Construtor recebendo uma instância do contexto de banco de dados
        public AtendimentosController(labmedicinebdContext labmedicinebd)
        {
            this.labmedicinebd = labmedicinebd;
        }

        [HttpPut]
        public ActionResult Atendimento([FromBody] IdentificadorPacienteMedicoDTO identificadorPacienteMedicoDTO)
        {
            AtendimentosModel atendimento = new AtendimentosModel();
            var paciente = labmedicinebd.Pacientes.Find(identificadorPacienteMedicoDTO.Identificador_Paciente);
            var medico = labmedicinebd.Medicos.Find(identificadorPacienteMedicoDTO.Identificador_Medico);
            if (paciente == null)
            {
                return NotFound("Identificador do paciente não existe no sistema!");
            }
            else if (medico == null)
            {
                return NotFound("Identificador do médico não existe no sistema!");
            }
            if (medico.EstadoSistema == EstadoSistemaEnum.Inativo)
            {
                return BadRequest("O médico com o identificador informado está com o estados INATIVO, por tanto não pode realizar o atendimento!");
            }
            // Atualização do Paciente
            paciente.StatusAtendimento = StatusAtendimentoEnum.ATENDIDO;
            paciente.TotalAtendimentosRealizados += 1;
            labmedicinebd.Pacientes.Attach(paciente);

            // Atualização do Médico
            medico.TotalAtendimentosRealizados += 1;
            labmedicinebd.Medicos.Attach(medico);

            // Atualização da Tabela Atendimento
            atendimento.Identificador_Medico = medico.Identificador;
            atendimento.Identificador_Paciente = paciente.Identificador;
            atendimento.especializacaoClinica = medico.EspecializacaoClinica;
            labmedicinebd.Atendimentos.Add(atendimento);

            // Save Changes para Atualização do médico e paciente. Adicionando o Histórico de atendimento no sistema!
            labmedicinebd.SaveChanges();
            return Ok("Atendimento realizado com sucesso! Os dados no sistema foram alterados!");
        }

        // Código a parte que não foi solicitado na documentação

        // Um get que retorna todos os atendimentos
        [HttpGet]
        public ActionResult ListaAtendimentos()
        {
            List<AtendimentosModel> atendimentos = new List<AtendimentosModel>();
            foreach (var atendimento in labmedicinebd.Atendimentos)
            {
                atendimentos.Add(atendimento);
            }
            return Ok(atendimentos);
        }

        // Um get que retorna todos os atendimentos do paciente com Id selecionado
        [HttpGet("Paciente/{idPaciente}")]
        public ActionResult AtendimentosPorPaciente(int idPaciente)
        {
            List<AtendimentosModel> atendimentos = new List<AtendimentosModel>();
            foreach (var atendimento in labmedicinebd.Atendimentos)
            {
                if (atendimento.Identificador_Paciente == idPaciente)
                {
                    atendimentos.Add(atendimento);
                }
            }
            if (atendimentos.Count > 0) 
            { 
                return Ok(atendimentos); 
            }
            return BadRequest("Identificador não encontrado no sistema");
        }

        // Um get que retorna todos os atendimentos do Médico com Id selecionado

        [HttpGet("Medico/{idMedico}")]
        public ActionResult AtendimentosPorMedico(int idMedico)
        {
            List<AtendimentosModel> atendimentos = new List<AtendimentosModel>();
            foreach (var atendimento in labmedicinebd.Atendimentos)
            {
                if (atendimento.Identificador_Medico == idMedico)
                {
                    atendimentos.Add(atendimento);
                }
            }
            if (atendimentos.Count > 0) 
            {
                return Ok(atendimentos);
            }
            return BadRequest("Identificador não encontrado no sistema");
        }
    }
}
