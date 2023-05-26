import static org.hamcrest.Matchers.*;
import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

import static org.hamcrest.MatcherAssert.assertThat;

class WordProcessorTest {

    @Mock
    private MyScanner mockedScanner;

    @InjectMocks
    private WordProcessor wordProcessor;

    @BeforeEach
    public void injectScanner() {
        wordProcessor = new WordProcessor();
    }

    @Test
    void readWords() throws IOException {
        String filename = "in.txt";
        //Mockito.when(mockedScanner.readWords(filename)).
        //        thenReturn(List.of("abbb,nblj khv   lb","123nbk, Kkbkzv"));
        assertThat(wordProcessor.readWords(filename),
                containsInAnyOrder("abbb", "Kkbkzv"));
        Mockito.verify(mockedScanner,Mockito.atLeastOnce()).readWords(filename);
    }

    @ParameterizedTest
    @CsvSource({"abb", "bbbba", "Hhcd", "cgkgvlbBsd"})
    void hasDoubledConsonants(String word) {
        assertTrue(wordProcessor.hasDoubledConsonants(word));
    }

    @ParameterizedTest
    @CsvSource({"abfb", "aba", "Ahcd", "cgkgvlbaaaBsd", "''","11d"})
    void hasNoDoubledConsonants(String word) {
        assertFalse(wordProcessor.hasDoubledConsonants(word));
    }

    @Test
    void findWordsInLine() {
        String line = "Bbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa, gg";
        assertThat(wordProcessor.findWordsInLine(line),
                not(hasItem(hasSize(greaterThan(30)))));
    }

    @Test
    void testFileNotFoundException() {
        assertThrows(FileNotFoundException.class,
                () -> wordProcessor.readWords("nonexistent_file.txt"));
    }

}