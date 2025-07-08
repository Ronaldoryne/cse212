public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
// Handle edge case: if length is 0 or negative, return empty array
        if (length <= 0)
        {
            return new double[0];
        }
        
        // Create array to store the multiples
        double[] multiples = new double[length];
        
        // Generate multiples using a loop
        // Loop from 1 to length (inclusive) to get 1st, 2nd, 3rd... multiples
        for (int i = 1; i <= length; i++)
        {
            // Calculate the multiple: number × current_position
            // Store in array at index (i-1) since array is 0-indexed
            multiples[i - 1] = number * i;
        }
        
        return multiples;
        // TODO Problem 1 End
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        // Handle edge cases
        if (data == null || data.Count == 0 || amount == 0)
        {
            return; // Nothing to rotate
        }
        
        // Handle case where amount equals list size (full rotation = no change)
        if (amount == data.Count)
        {
            return; // List remains the same after full rotation
        }
        
        // Calculate the split point
        // If we rotate right by 'amount', the split point is at (data.Count - amount)
        int splitIndex = data.Count - amount;
        
        // Extract the two parts using GetRange
        // Left part: from beginning to split point (these will move to the back)
        List<int> leftPart = data.GetRange(0, splitIndex);
        
        // Right part: from split point to end (these will move to the front)
        List<int> rightPart = data.GetRange(splitIndex, amount);
        
        // Clear the original list
        data.Clear();
        
        // Add the right part first (moves to front)
        data.AddRange(rightPart);
        
        // Add the left part second (moves to back)
        data.AddRange(leftPart);
        
        // TODO Problem 2 End
    }
}