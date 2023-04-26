import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.sql.Array;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.*;
import static org.junit.jupiter.api.Assertions.*;

class MainTest {
    static File tempFile;

    @BeforeAll
    static public void setupTempFile() throws IOException {
        tempFile = File.createTempFile("temp", ".txt");
        tempFile.deleteOnExit();
        PrintWriter fileWriter = new PrintWriter(tempFile);
        fileWriter.println("kkbkzv, abbb,nblj khv   lb\n123nbk, kkbkzv");
        fileWriter.close();
    }

    private final ByteArrayOutputStream testOut = new ByteArrayOutputStream();
    private final PrintStream originalOut = System.out;

    @BeforeEach
    public void setUpStreams() {
        System.setOut(new PrintStream(testOut));
    }

    @AfterEach
    public void restoreStreams() {
        System.setOut(originalOut);
    }

    @Test
    void processFile() throws FileNotFoundException {
        assertThat(Main.processFile(tempFile.getPath()),
                both(hasSize(2)).and(contains("abbb", "kkbkzv")));
    }

    @Test
    void noFileSpecified(){
        Main.main(new String[0]);
        String expected = "Filename not specified." + System.lineSeparator();
        assertEquals(expected, testOut.toString());
    }
}