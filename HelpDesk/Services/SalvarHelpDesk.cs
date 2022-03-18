using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpDesk.Services
{
    public class SalvarHelpDesk
    {
        private readonly HttpClient client = new HttpClient();
        public async Task<string> SalvarHd(Models.HelpDesk hd)
        {
            byte[] buffer = new byte[16 * 1024];
            BufferedStream stream = new BufferedStream(hd.Anexo.ArquivoUpload.OpenReadStream());
            MemoryStream ms;
            using ( ms = new MemoryStream())
            {
                int read;
                while((read = stream.Read(buffer, 0, buffer.Length)) > 0){
                    ms.Write(buffer, 0, read);
                }
            }
            var base64String = Convert.ToBase64String(ms.ToArray());
            hd.Anexo.Arquivo = base64String;
            hd.Anexo.NomeArquivo = hd.Anexo.ArquivoUpload.FileName;
            hd.Descricao = hd.Assunto + "\n" + hd.Descricao + "\n"+ hd.Nome;
            string jsonString = JsonSerializer.Serialize(hd);
            try
            {
                HttpContent content = new StringContent(jsonString);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                HttpResponseMessage response = await client.PostAsync("http://192.168.0.228:42510/api/helpDesk/hd", content);
                return await response.Content.ReadAsStringAsync();               
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
