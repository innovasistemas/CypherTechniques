namespace CypherTechniques.EncodeDecode
{
    class InvertirTexto
    {
        private readonly string texto;


        public InvertirTexto()
        {
            texto = "";
        }

        public InvertirTexto(string phrase)
        {
            texto = phrase;
        }

		public string InvertirCadena() {
			string textoInvertido = "";
			for (int i = texto.Length - 1; i >= 0; i--) {
				textoInvertido += texto.Substring(i, 1);
			}
			return textoInvertido;
		}

		public string RevertirCadena() {
			string textoRevertido = "";
			for (int i = 0; i < texto.Length; i++) {
				textoRevertido = texto.Substring(i, 1) + textoRevertido;
			}
			return textoRevertido;
		}
	}
}