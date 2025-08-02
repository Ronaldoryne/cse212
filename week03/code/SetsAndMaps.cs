using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var wordSet = new HashSet<string>(words);
        var result = new List<string>();
        var processed = new HashSet<string>();

        foreach (var word in words)
        {
            // Skip if already processed or if both letters are the same
            if (processed.Contains(word) || word[0] == word[1])
                continue;

            // Create the reverse of the word
            var reverse = new string(new char[] { word[1], word[0] });

            // Check if reverse exists in the set and hasn't been processed
            if (wordSet.Contains(reverse) && !processed.Contains(reverse))
            {
                result.Add($"{word} & {reverse}");
                processed.Add(word);
                processed.Add(reverse);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // The degree is in the 4th column (index 3)
            if (fields.Length > 3)
            {
                var degree = fields[3].Trim();
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Convert to lowercase and remove spaces
        word1 = word1.ToLower().Replace(" ", "");
        word2 = word2.ToLower().Replace(" ", "");

        // If lengths are different after cleaning, they can't be anagrams
        if (word1.Length != word2.Length)
            return false;

        // Count frequency of each character in word1
        var charCount = new Dictionary<char, int>();
        
        foreach (char c in word1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        // Subtract frequency for each character in word2
        foreach (char c in word2)
        {
            if (!charCount.ContainsKey(c))
                return false; // Character not in word1
            
            charCount[c]--;
            
            if (charCount[c] < 0)
                return false; // More occurrences in word2 than word1
        }

        // Check if all counts are zero
        return charCount.Values.All(count => count == 0);
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        try
        {
            const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
            using var client = new HttpClient();
            using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = client.Send(getRequestMessage);
            using var jsonStream = response.Content.ReadAsStream();
            using var reader = new StreamReader(jsonStream);
            var json = reader.ReadToEnd();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            var result = new List<string>();
            
            if (featureCollection?.Features != null)
            {
                foreach (var feature in featureCollection.Features)
                {
                    if (feature?.Properties != null)
                    {
                        var place = feature.Properties.Place ?? "Unknown location";
                        var magnitude = feature.Properties.Mag.HasValue 
                            ? feature.Properties.Mag.Value.ToString("F1") 
                            : "Unknown";
                        result.Add($"{place} - Mag {magnitude}");
                    }
                }
            }

            return result.ToArray();
        }
        catch (Exception)
        {
            // Return empty array if there are any issues with the API call or parsing
            return new string[0];
        }
    }
}

// Classes needed for JSON deserialization of earthquake data
public class FeatureCollection
{
    public string Type { get; set; }
    public Metadata Metadata { get; set; }
    public Feature[] Features { get; set; }
}

public class Metadata
{
    public long Generated { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Api { get; set; }
    public int Count { get; set; }
}

public class Feature
{
    public string Type { get; set; }
    public Properties Properties { get; set; }
    public Geometry Geometry { get; set; }
    public string Id { get; set; }
}

public class Properties
{
    public double? Mag { get; set; }
    public string Place { get; set; }
    public long Time { get; set; }
    public long Updated { get; set; }
    public int? Tz { get; set; }
    public string Url { get; set; }
    public string Detail { get; set; }
    public int? Felt { get; set; }
    public double? Cdi { get; set; }
    public double? Mmi { get; set; }
    public string Alert { get; set; }
    public string Status { get; set; }
    public int Tsunami { get; set; }
    public int Sig { get; set; }
    public string Net { get; set; }
    public string Code { get; set; }
    public string Ids { get; set; }
    public string Sources { get; set; }
    public string Types { get; set; }
    public int? Nst { get; set; }
    public double? Dmin { get; set; }
    public double? Rms { get; set; }
    public double? Gap { get; set; }
    public string MagType { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
}

public class Geometry
{
    public string Type { get; set; }
    public double[] Coordinates { get; set; }
}