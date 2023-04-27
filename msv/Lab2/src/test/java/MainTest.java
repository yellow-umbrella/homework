import static org.testng.Assert.*;
import org.testng.annotations.*;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.*;

public class MainTest {
    static File tempFile;

    @BeforeClass
    public static void setupTempFile() throws IOException {
        tempFile = File.createTempFile("temp", ".txt");
        PrintWriter fileWriter = new PrintWriter(tempFile);
        fileWriter.println("kkbkzv, abbb,nblj khv   lb\n123nbk, kkbkzv");
        fileWriter.close();
    }

    @AfterClass
    public static void deleteTempFile() throws IOException {
        tempFile.deleteOnExit();
    }

    private final ByteArrayOutputStream testOut = new ByteArrayOutputStream();
    private final PrintStream originalOut = System.out;

    @BeforeMethod
    public void setUpStreams() {
        System.setOut(new PrintStream(testOut));
    }

    @AfterMethod
    public void restoreStreams() {
        System.setOut(originalOut);
    }

    @Test (groups = {"main"})
    void processFile() throws FileNotFoundException {
        assertThat(Main.processFile(tempFile.getPath()),
                both(hasSize(2)).and(contains("abbb", "kkbkzv")));
    }

    @Test(groups = {"main"})
    void noFileSpecified(){
        //System.setOut(new PrintStream(testOut));
        Main.main(new String[0]);
        String expected = "Filename not specified." + System.lineSeparator();
        assertEquals(expected, testOut.toString());
    }
}