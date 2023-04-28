package com.manty.tests;

import static org.testng.Assert.*;

import com.manty.lab3.Main;
import org.testng.annotations.*;

import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.*;

public class MainTest {
    static File tempFile;

    @BeforeClass
    static public void setupTempFile() throws IOException {
        tempFile = File.createTempFile("temp", ".txt");
        tempFile.deleteOnExit();
        PrintWriter fileWriter = new PrintWriter(tempFile);
        fileWriter.println("kkbkzv, abbb,nblj khv   lb\n123nbk, kkbkzv");
        fileWriter.close();
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

    @Test
    void processFile() throws IOException {
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