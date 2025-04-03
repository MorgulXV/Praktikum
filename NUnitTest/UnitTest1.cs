namespace NUnitTest
{
    public class Tests
    {
        class GFG
        {
            int[] unsortedArray = { 3000, 38, 232, -23, 44, 0, 33, 4242, 14, 1, 300, -999, 500 };
            int[] sortedArray = { -999, -23, 0, 1, 14, 33, 38, 44, 232, 300, 500, 3000, 4242 };
            public void gnomeSort(int[] arr, int len)
            {
                int index = 0;

                while (index < len)
                {
                    if (index == 0)
                        index++;
                    if (arr[index] >= arr[index - 1])
                        index++;
                    else
                    {
                        int temp = 0;
                        temp = arr[index];
                        arr[index] = arr[index - 1];
                        arr[index - 1] = temp;
                        index--;
                    }
                }
                return;
            }

            [SetUp]
            public void Setup()
            {
                
            }

            [Test]
            public void GnomeSortTest()
            {
                GFG gfg = new GFG();
                gfg.gnomeSort(gfg.unsortedArray, unsortedArray.Length);
                Assert.That(sortedArray, Is.EqualTo(unsortedArray), "Gnome sort isn't implemented in a correct way");
            }
        }
    }
}