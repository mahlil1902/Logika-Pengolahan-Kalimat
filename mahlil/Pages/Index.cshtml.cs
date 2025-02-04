using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string InputText { get; set; }

    public string OutputText { get; set; }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(InputText))
        {
            OutputText = ProcessText(InputText);
        }
    }

    private string ProcessText(string input)
    {
        // Menghapus spasi dari input agar penghitungan global
        string cleanedInput = input.Replace(" ", "");

        // Hitung frekuensi setiap huruf dalam seluruh input
        Dictionary<char, int> frequency = new Dictionary<char, int>();
        List<char> order = new List<char>(); // Untuk menyimpan urutan kemunculan pertama kali

        foreach (char ch in cleanedInput)
        {
            if (frequency.ContainsKey(ch))
                frequency[ch]++;
            else
            {
                frequency[ch] = 1;
                order.Add(ch); // Simpan urutan pertama kali muncul
            }
        }

        // Urutkan berdasarkan jumlah kemunculan (descending)
        // Jika jumlah sama, maka urutkan berdasarkan urutan pertama kali muncul
        var sortedChars = frequency
            .OrderByDescending(kvp => kvp.Value) // Urutkan berdasarkan jumlah kemunculan (banyak ke sedikit)
            .ThenBy(kvp => order.IndexOf(kvp.Key)) // Jika jumlah sama, urutkan berdasarkan urutan awal muncul di input
            .ToList();

        // Bangun hasil output
        string repeatedChars = "";
        string singleChars = "";

        foreach (var kvp in sortedChars)
        {
            if (kvp.Value > 1)
                repeatedChars += kvp.Key.ToString() + kvp.Value; // Huruf + jumlah kemunculan
            else
                singleChars += kvp.Key; // Huruf yang hanya muncul sekali
        }

        // Gabungkan hasil akhir dengan spasi antara dua bagian
        return repeatedChars + " " + singleChars;
    }
}