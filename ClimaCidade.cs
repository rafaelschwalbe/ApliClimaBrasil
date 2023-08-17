namespace WebApplicationApiClime
{
    public class ClimaCidade
    {
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Atualizado_Em { get; set; }
        public Clima[] Clima { get; set; }

        //public string Data { get; set; }
        //public string Condicao { get; set; }
        //public string Condicao_Desc { get; set; }
        //public int Min { get; set; }
        //public int Max { get; set; }
        //public int Indice_Uv { get; set; }
    }

    public class Clima 
    {
        public string Data { get; set; }
        public string Condicao { get; set; }
        public string Condicao_Desc { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Indice_Uv { get; set; }
    }
}
