﻿using LABMedicine.Enumerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LABMedicine.Models
{
    [Table("Medicos")]
    public class MedicosModel : PessoaModel
    {
        [Required(ErrorMessage = "Por favor insira uma Instituição de Ensino existente!"), Column("Instituicao_de_Ensino")]
        public string InstituicaoEnsino { get; set; }

        [Required(ErrorMessage = "Por favor insira um cadastro do CRM/UF correto!"), Column("Cadastro_CRM/UF")]
        public string CadastroCRM_UF { get; set;}

        [Required(ErrorMessage = $"Por favor insira uma epescialização clinica existente no sistema!"), Column("Especializacao_Clinica")]
        public EspecializacaoClinicaEnum EspecializacaoClinica { get; set; }

        [Required(ErrorMessage = "Informe um estado válido: ATIVO / INATIVO"), Column("Estano_no_Sistema")]
        public EstadoSistemaEnum EstadoSistema { get; set; }

        [Column("Total_de_Atendimentos_Realizados")]
        public int TotalAtendimentosRealizados { get; set; }
    }
}
