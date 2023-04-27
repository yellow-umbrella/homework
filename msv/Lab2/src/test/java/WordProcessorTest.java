import static org.testng.Assert.*;
import org.testng.annotations.*;
import static org.hamcrest.Matchers.*;
import static org.hamcrest.MatcherAssert.assertThat;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;


public class WordProcessorTest {

    @DataProvider (name = "with-doubled-consonants-provider")
    public Object[][] doubledConsonants(){
        return new Object[][] {{"abb"}, {"bbbba"}, {"Hhcd"}};
    }

    @DataProvider (name = "without-doubled-consonants-provider")
    public Object[][] noDoubledConsonants(){
        return new Object[][] {{"abfb"}, {"aba"}, {"Ahcd"}, {"cgkgvlbaaaBsd"}, {""},{"11d"}};
    }

    @Test(groups = {"word processor"}, dataProvider = "with-doubled-consonants-provider")
    void hasDoubledConsonants(String word) {
        assertTrue(WordProcessor.hasDoubledConsonants(word));
    }

    @Test(groups = {"word processor"}, dataProvider = "without-doubled-consonants-provider")
    void hasNoDoubledConsonants(String word) {
        assertFalse(WordProcessor.hasDoubledConsonants(word));
    }

    @Test(groups = {"word processor"})
    void findWordsInLine() {
        String line = "Bbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa, gg";
        assertThat(WordProcessor.findWordsInLine(line),
                not(hasItem(hasSize(greaterThan(30)))));
    }

    @Test(groups = {"word processor", "exception"})
    void testFileNotFoundException() {
        assertThrows(FileNotFoundException.class,
                () -> WordProcessor.readWords("nonexistent_file.txt"));
    }

    @Test(groups = {"word processor"})
    void readWords() throws IOException {
        File tempFile = File.createTempFile("temp", ".txt");
        tempFile.deleteOnExit();
        PrintWriter fileWriter = new PrintWriter(tempFile);
        fileWriter.println("abbb,nblj khv   lb\n123nbk, Kkbkzv");
        fileWriter.close();

        assertThat(WordProcessor.readWords(tempFile.getPath()),
                containsInAnyOrder("abbb", "Kkbkzv"));
    }
}