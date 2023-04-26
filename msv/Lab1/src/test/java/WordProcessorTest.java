import static org.hamcrest.Matchers.*;
import static org.junit.jupiter.api.Assertions.*;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;

import static org.hamcrest.MatcherAssert.assertThat;

class WordProcessorTest {

    @ParameterizedTest
    @CsvSource({"abb", "bbbba", "Hhcd", "cgkgvlbBsd"})
    void hasDoubledConsonants(String word) {
        assertTrue(WordProcessor.hasDoubledConsonants(word));
    }

    @ParameterizedTest
    @CsvSource({"abfb", "aba", "Ahcd", "cgkgvlbaaaBsd", "''","11d"})
    void hasNoDoubledConsonants(String word) {
        assertFalse(WordProcessor.hasDoubledConsonants(word));
    }

    @Test
    void findWordsInLine() {
        String line = "Bbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa, gg";
        assertThat(WordProcessor.findWordsInLine(line),
                not(hasItem(hasSize(greaterThan(30)))));
    }

    @Test
    void testFileNotFoundException() {
        assertThrows(FileNotFoundException.class,
                () -> WordProcessor.readWords("nonexistent_file.txt"));
    }

    @Test
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