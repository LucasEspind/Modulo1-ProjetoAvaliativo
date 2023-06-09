﻿using LABMedicine.Enumerator;

namespace LABMedicine.DTO
{
    public class AdicionarMedicoDTO
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string InstituicaoEnsino { get; set; }
        public string CadastroCRM_UF { get; set; }
        public EspecializacaoClinicaEnum EspecializacaoClinica { get; set; }
    }
}
