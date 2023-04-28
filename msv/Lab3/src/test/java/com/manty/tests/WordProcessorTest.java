package com.manty.tests;

import static org.testng.Assert.*;
import org.testng.annotations.*;

import static org.hamcrest.Matchers.*;
import static org.hamcrest.MatcherAssert.assertThat;

import org.mockito.*;


import com.manty.lab3.MyScanner;
import com.manty.lab3.WordProcessor;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class WordProcessorTest {
    @Mock
    private MyScanner mockedScanner = new MyScanner();

    @InjectMocks
    private WordProcessor wordProcessor = new WordProcessor();

    @BeforeMethod(alwaysRun = true)
    public void initMocks() {
        MockitoAnnotations.openMocks(this);
    }

    @DataProvider(name = "readWordsTestData")
    public Object[][] testData() {
        return new Object[][]{
                {"in1.txt", List.of("abbb,nblj khv   lb", "123nbk, Kkbkzv"),
                        new String[]{"abbb", "Kkbkzv"}},
                {"in2.txt", List.of("foo bar", "baz qux"),
                        new String[]{}},
                {"in3.txt", List.of("a", "bb", "ccc", "dddd", "eeeee"),
                        new String[]{"dddd", "bb", "ccc"}}
        };
    }

    @Test(dataProvider = "readWordsTestData", groups = {"mock"})
    void readWords(String filename, List<String> lines, String[] expectedWords) throws IOException {
        Mockito.when(mockedScanner.readLines(filename)).thenReturn(lines);
        assertThat(wordProcessor.readWords(filename), containsInAnyOrder(expectedWords));

        Mockito.verify(mockedScanner, Mockito.atLeastOnce()).readLines(filename);
    }

    @Test(groups = {"mock"})
    void readWordsIOException() throws IOException {
        String filename = "in.txt";

        Mockito.when(mockedScanner.readLines(filename)).thenReturn(null);
        assertThrows(IOException.class,
                () -> wordProcessor.readWords(filename));

        Mockito.verify(mockedScanner, Mockito.atLeastOnce()).readLines(filename);
    }

    @Test(groups = {"mock"})
    void readWordsFileNotFoundException() throws IOException {
        String filename = "nonexistent_file.txt";

        Mockito.when(mockedScanner.readLines(filename)).
                thenThrow(FileNotFoundException.class);
        assertThrows(FileNotFoundException.class,
                () -> wordProcessor.readWords(filename));

        Mockito.verify(mockedScanner, Mockito.atLeastOnce()).readLines(filename);
    }

    @Test(groups = {"mock"})
    void readWordsDefault() throws IOException {
        String filename = "in1.txt";

        assertEquals(wordProcessor.readWords(filename), new HashSet<String>());
        Mockito.verify(mockedScanner, Mockito.atLeastOnce()).readLines(filename);
    }

    @Spy
    WordProcessor spiedWordProcessor = new WordProcessor();

    @Test(groups = {"spy"})
    void spiedFindWordsInLine() {
        String line = "gvb gg kvhn, kbknbco, Hhgc";
        Set<String> expected1 = Set.of("Hhgc", "gg");
        Set<String> expected2 = Set.of("gvb", "gg", "kvhn", "kbknbco", "Hhgc");

        assertEquals(spiedWordProcessor.findWordsInLine(line), expected1);
        Mockito.verify(spiedWordProcessor, Mockito.times(5))
                .hasDoubledConsonants(Mockito.anyString());

        Mockito.doReturn(true).when(spiedWordProcessor).hasDoubledConsonants(Mockito.anyString());
        assertEquals(spiedWordProcessor.findWordsInLine(line), expected2);

        Mockito.doCallRealMethod().when(spiedWordProcessor).hasDoubledConsonants(Mockito.anyString());
        assertEquals(spiedWordProcessor.findWordsInLine(line), expected1);
    }


    @Test
    void findWordsInLine() {
        String line = "Bbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa, gg";
        assertThat(wordProcessor.findWordsInLine(line),
                not(hasItem(hasSize(greaterThan(30)))));
    }

    @DataProvider (name = "with-doubled-consonants-provider")
    public Object[][] doubledConsonants(){
        return new Object[][] {{"abb"}, {"bbbba"}, {"Hhcd"}};
    }

    @DataProvider (name = "without-doubled-consonants-provider")
    public Object[][] noDoubledConsonants(){
        return new Object[][] {{"abfb"}, {"aba"}, {"Ahcd"}, {"cgkgvlbaaaBsd"}, {""},{"11d"}};
    }

    @Test(dataProvider = "with-doubled-consonants-provider")
    void hasDoubledConsonants(String word) {
        assertTrue(wordProcessor.hasDoubledConsonants(word));
    }

    @Test(dataProvider = "without-doubled-consonants-provider")
    void hasNoDoubledConsonants(String word) {
        assertFalse(wordProcessor.hasDoubledConsonants(word));
    }

}