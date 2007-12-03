using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Edu.Psu.Ist.DynamicMail
{
    

    

        /*

       Porter stemmer in CSharp, based on the Java port. The original paper is in

           Porter, 1980, An algorithm for suffix stripping, Program, Vol. 14,
           no. 3, pp 130-137,

       See also http://www.tartarus.org/~martin/PorterStemmer

       History:

       Release 1

       Bug 1 (reported by Gonzalo Parra 16/10/99) fixed as marked below.
       The words 'aed', 'eed', 'oed' leave k at 'a' for step 3, and b[k-1]
       is then out outside the bounds of b.

       Release 2

       Similarly,

       Bug 2 (reported by Steve Dyrdahl 22/2/00) fixed as marked below.
       'ion' by itself leaves j = -1 in the test for 'ion' in step 5, and
       b[j] is then outside the bounds of b.

       Release 3

       Considerably revised 4/9/00 in the light of many helpful suggestions
       from Brian Goetz of Quiotix Corporation (brian@quiotix.com).

       Release 4

    */

        /**
          * Stemmer, implementing the Porter Stemming Algorithm
          *
          * The Stemmer class transforms a word into its root form.  The input
          * word can be provided a character at time (by calling add()), or at once
          * by calling one of the various stem(something) methods.
          */
        
        /// <summary>
        /// Class that performs the Porter Stemmer Algorithm
        /// </summary>
        class Stemmer
        {
            /// <summary>
            /// Indexes instance
            /// </summary>
            private Indexes InvertedIndexes = Indexes.Instance;

            /// <summary>
            /// array list to hold the stop list
            /// </summary>
            public static ArrayList StopList = new ArrayList();


            private char[] b;
            private int i,     /* offset into b */
                i_end, /* offset to end of stemmed word */
                j, k;
            private static int INC = 50;
            /* unit of size whereby b is increased */

            /// <summary>
            /// Public Constructor
            /// </summary>
            public Stemmer()
            {
                b = new char[INC];
                i = 0;
                i_end = 0;

                initilizeStopList();
            }

            /// <summary>
            /// populates the stoplist arraylist from the stoplist.txt file
            /// </summary>
            private void initilizeStopList()
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                StreamReader sr = new StreamReader("C:\\Documents and Settings\\David\\Desktop\\Fall07\\IST496\\GoogleCode\\DynamicMailCombined\\DynamicMailParser\\stoplist.txt");

                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    Stemmer.StopList.Add(line);
                }
            }

            /**
             * Add a character to the word being stemmed.  When you are finished
             * adding characters, you can call stem(void) to stem the word.
             */
            /// <summary>
            /// Add a character to the word being stemmed
            /// </summary>
            /// <remarks>
            /// When you are finished adding characters, you can call stem(void) to stem the word.
            /// </remarks>
            public void add(char ch)
            {
                if (i == b.Length)
                {
                    char[] new_b = new char[i + INC];
                    for (int c = 0; c < i; c++)
                        new_b[c] = b[c];
                    b = new_b;
                }
                b[i++] = ch;
            }


            /** Adds wLen characters to the word being stemmed contained in a portion
             * of a char[] array. This is like repeated calls of add(char ch), but
             * faster.
             */

            /// <summary>
            /// Adds wLen characters to the word being stemmed
            /// </summary>
            /// <param name="w">Character to be added</param>
            /// <param name="wLen">Amount of times char w is added to word being stemmed</param>
            /// <remarks>
            /// This is similar to repeated calling add(char ch).  This is faster then multiple calls.
            /// </remarks>
            public void add(char[] w, int wLen)
            {
                if (i + wLen >= b.Length)
                {
                    char[] new_b = new char[i + wLen + INC];
                    for (int c = 0; c < i; c++)
                        new_b[c] = b[c];
                    b = new_b;
                }
                for (int c = 0; c < wLen; c++)
                    b[i++] = w[c];
            }

            /**
             * After a word has been stemmed, it can be retrieved by toString(),
             * or a reference to the internal buffer can be retrieved by getResultBuffer
             * and getResultLength (which is generally more efficient.)
             */

            /// <summary>
            /// Returns the String representation of the word after it is stemmed
            /// </summary>
            /// <returns>
            /// Returns the String representation of the word after it is stemmed
            /// </returns>
            /// <remarks>
            /// reference to the internal buffer can be retrieved by getResultBuffer and getResultLength (which is generally more efficient.)
            /// </remarks>
            public override string ToString()
            {
                return new String(b, 0, i_end);
            }

            /**
             * Returns the length of the word resulting from the stemming process.
             */

            /// <summary>
            /// Returns the length of the word resulting from the stemming process
            /// </summary>
            /// <returns>
            /// Returns the length of the word resulting from the stemming process
            /// </returns>
            public int getResultLength()
            {
                return i_end;
            }

            /**
             * Returns a reference to a character buffer containing the results of
             * the stemming process.  You also need to consult getResultLength()
             * to determine the length of the result.
             */

            /// <summary>
            /// Returns a reference to a character buffer containing the results of the stemming process
            /// </summary>
            /// <returns>
            /// Returns a reference to a character buffer containing the results of the stemming process
            /// </returns>
            /// <remarks>
            /// You also need to consult getResultLength() to determine the length of the result.
            /// </remarks>
            public char[] getResultBuffer()
            {
                return b;
            }

            /* cons(i) is true <=> b[i] is a consonant. */

            /// <summary>
            /// Determine if b[i] is a consonant
            /// </summary>
            /// <param name="i">Position in array b[]</param>
            /// <returns>If b[i] is a consonant returns true.  If b[i] is a vowel returns false</returns>
            private bool cons(int i)
            {
                switch (b[i])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u': return false;
                    case 'y': return (i == 0) ? true : !cons(i - 1);
                    default: return true;
                }
            }

            /* m() measures the number of consonant sequences between 0 and j. if c is
               a consonant sequence and v a vowel sequence, and <..> indicates arbitrary
               presence,

                  <c><v>       gives 0
                  <c>vc<v>     gives 1
                  <c>vcvc<v>   gives 2
                  <c>vcvcvc<v> gives 3
                  ....
            */

            /// <summary>
            /// Measures the number of consonant sequences between 0 and j
            /// </summary>
            /// <returns>Number of consonant/vowel sequences</returns>
            /// <remarks>
            /// f c is a consonant sequence and v a vowel sequence, and <..> indicates arbitrary presence,
            ///      <c><v>       gives 0
            ///      <c>vc<v>     gives 1
            ///      <c>vcvc<v>   gives 2
            ///      <c>vcvcvc<v> gives 3
            ///      ....
            /// </remarks>
            private int m()
            {
                int n = 0;
                int i = 0;
                while (true)
                {
                    if (i > j) return n;
                    if (!cons(i)) break; i++;
                }
                i++;
                while (true)
                {
                    while (true)
                    {
                        if (i > j) return n;
                        if (cons(i)) break;
                        i++;
                    }
                    i++;
                    n++;
                    while (true)
                    {
                        if (i > j) return n;
                        if (!cons(i)) break;
                        i++;
                    }
                    i++;
                }
            }

            /* vowelinstem() is true <=> 0,...j contains a vowel */
            /// <summary>
            /// Determines if stem contains a vowel
            /// </summary>
            /// <returns>Returns true if stem contains vowel else Returns false</returns>
            private bool vowelinstem()
            {
                int i;
                for (i = 0; i <= j; i++)
                    if (!cons(i))
                        return true;
                return false;
            }

            /* doublec(j) is true <=> j,(j-1) contain a double consonant. */

            /// <summary>
            /// Determines if character in position J and J-1 are consonants
            /// </summary>
            /// <param name="j">Position in array b[]</param>
            /// <returns>Returns true if J and J-1</returns>
            private bool doublec(int j)
            {
                if (j < 1)
                    return false;
                if (b[j] != b[j - 1])
                    return false;
                return cons(j);
            }

            /* cvc(i) is true <=> i-2,i-1,i has the form consonant - vowel - consonant
               and also if the second c is not w,x or y. this is used when trying to
               restore an e at the end of a short word. e.g.

                  cav(e), lov(e), hop(e), crim(e), but
                  snow, box, tray.

            */

            /// <summary>
            /// Determines if b[i-2],b[i-1],b[i] is a consonant-vowel-consonant pattern
            /// </summary>
            /// <param name="i">The position of the last character in the 3 letter pattern</param>
            /// <returns>Returns true if b[i-2],b[i-1],b[i] is a consonantt-vowel-consonant pattern and the second consonant is not w,x, or y</returns>
            /// <remarks>
            /// cvc(i) is true <=> i-2,i-1,i has the form consonant - vowel - consonant and also if the second c is not w,x or y. this is used when trying to restore an e at the end of a short word. e.g.
            /// cav(e), lov(e), hop(e), or crim(e), but not sno(w), bo(x), tra(y).
            /// </remarks>
            private bool cvc(int i)
            {
                if (i < 2 || !cons(i) || cons(i - 1) || !cons(i - 2))
                    return false;
                int ch = b[i];
                if (ch == 'w' || ch == 'x' || ch == 'y')
                    return false;
                return true;
            }

            /// <summary>
            /// No comments given (requires explaination)
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            private bool ends(String s)
            {
                int l = s.Length;
                int o = k - l + 1;
                if (o < 0)
                    return false;
                char[] sc = s.ToCharArray();
                for (int i = 0; i < l; i++)
                    if (b[o + i] != sc[i])
                        return false;
                j = k - l;
                return true;
            }

            /* setto(s) sets (j+1),...k to the characters in the string s, readjusting
               k. */

            /// <summary>
            /// sets (j+1),...k to the characters in the string s, readjusting k.
            /// </summary>
            /// <param name="s"></param>
            private void setto(String s)
            {
                int l = s.Length;
                int o = j + 1;
                char[] sc = s.ToCharArray();
                for (int i = 0; i < l; i++)
                    b[o + i] = sc[i];
                k = j + l;
            }

            /* r(s) is used further down. */
            private void r(String s)
            {
                if (m() > 0)
                    setto(s);
            }

            /* step1() gets rid of plurals and -ed or -ing. e.g.
                   caresses  ->  caress
                   ponies    ->  poni
                   ties      ->  ti
                   caress    ->  caress
                   cats      ->  cat

                   feed      ->  feed
                   agreed    ->  agree
                   disabled  ->  disable

                   matting   ->  mat
                   mating    ->  mate
                   meeting   ->  meet
                   milling   ->  mill
                   messing   ->  mess

                   meetings  ->  meet

            */

            /// <summary>
            /// Removes endings such as plurals or -ing/-ed
            /// </summary>
            /// <example>
            /// caresses  ->  caress
            /// ponies    ->  poni
            /// ties      ->  ti
            /// caress    ->  caress
            /// cats      ->  cat
            /// 
            /// feed      ->  feed
            /// agreed    ->  agree
            /// disabled  ->  disable
            /// 
            ///  matting   ->  mat
            ///  mating    ->  mate
            ///  meeting   ->  meet
            ///  milling   ->  mill
            ///  messing   ->  mess
            /// </example>
            private void step1()
            {
                if (b[k] == 's')
                {
                    if (ends("sses"))
                        k -= 2;
                    else if (ends("ies"))
                        setto("i");
                    else if (b[k - 1] != 's')
                        k--;
                }
                if (ends("eed"))
                {
                    if (m() > 0)
                        k--;
                }
                else if ((ends("ed") || ends("ing")) && vowelinstem())
                {
                    k = j;
                    if (ends("at"))
                        setto("ate");
                    else if (ends("bl"))
                        setto("ble");
                    else if (ends("iz"))
                        setto("ize");
                    else if (doublec(k))
                    {
                        k--;
                        int ch = b[k];
                        if (ch == 'l' || ch == 's' || ch == 'z')
                            k++;
                    }
                    else if (m() == 1 && cvc(k)) setto("e");
                }
            }

            /* step2() turns terminal y to i when there is another vowel in the stem. */

            /// <summary>
            /// Turns terminal y to i when there is another vowel in the stem
            /// </summary>
            private void step2()
            {
                if (ends("y") && vowelinstem())
                    b[k] = 'i';
            }

            /* step3() maps double suffices to single ones. so -ization ( = -ize plus
               -ation) maps to -ize etc. note that the string before the suffix must give
               m() > 0. */

            /// <summary>
            /// Maps double suffices
            /// </summary>
            /// <example>
            /// -ization = -ize + -ation
            /// Note: string before suffix must give m() > 0
            /// </example>
            private void step3()
            {
                if (k == 0)
                    return;

                /* For Bug 1 */
                switch (b[k - 1])
                {
                    case 'a':
                        if (ends("ational")) { r("ate"); break; }
                        if (ends("tional")) { r("tion"); break; }
                        break;
                    case 'c':
                        if (ends("enci")) { r("ence"); break; }
                        if (ends("anci")) { r("ance"); break; }
                        break;
                    case 'e':
                        if (ends("izer")) { r("ize"); break; }
                        break;
                    case 'l':
                        if (ends("bli")) { r("ble"); break; }
                        if (ends("alli")) { r("al"); break; }
                        if (ends("entli")) { r("ent"); break; }
                        if (ends("eli")) { r("e"); break; }
                        if (ends("ousli")) { r("ous"); break; }
                        break;
                    case 'o':
                        if (ends("ization")) { r("ize"); break; }
                        if (ends("ation")) { r("ate"); break; }
                        if (ends("ator")) { r("ate"); break; }
                        break;
                    case 's':
                        if (ends("alism")) { r("al"); break; }
                        if (ends("iveness")) { r("ive"); break; }
                        if (ends("fulness")) { r("ful"); break; }
                        if (ends("ousness")) { r("ous"); break; }
                        break;
                    case 't':
                        if (ends("aliti")) { r("al"); break; }
                        if (ends("iviti")) { r("ive"); break; }
                        if (ends("biliti")) { r("ble"); break; }
                        break;
                    case 'g':
                        if (ends("logi")) { r("log"); break; }
                        break;
                    default:
                        break;
                }
            }

            /* step4() deals with -ic-, -full, -ness etc. similar strategy to step3. */

            /// <summary>
            /// Maps -ic-, -full, -ness and additional suffices
            /// </summary>
            /// <remarks>
            /// Deals with -icate, -ative, -alize, -iciti, -ical, -ful, -ness
            /// </remarks>
            private void step4()
            {
                switch (b[k])
                {
                    case 'e':
                        if (ends("icate")) { r("ic"); break; }
                        if (ends("ative")) { r(""); break; }
                        if (ends("alize")) { r("al"); break; }
                        break;
                    case 'i':
                        if (ends("iciti")) { r("ic"); break; }
                        break;
                    case 'l':
                        if (ends("ical")) { r("ic"); break; }
                        if (ends("ful")) { r(""); break; }
                        break;
                    case 's':
                        if (ends("ness")) { r(""); break; }
                        break;
                }
            }

            /* step5() takes off -ant, -ence etc., in context <c>vcvc<v>. */
            /// <summary>
            /// Removes endings like -ant and -ence if context is cvcvcv
            /// </summary>
            /// <remarks>
            /// Deals with -al, -ance, -ence, -er, -ic, -able, ible, -ant
            /// -ement, -ment, -ent, -ion, -ou, -ism, -iti, -ous, -ive, -ize
            /// </remarks>
            private void step5()
            {
                if (k == 0)
                    return;

                /* for Bug 1 */
                switch (b[k - 1])
                {
                    case 'a':
                        if (ends("al")) break; return;
                    case 'c':
                        if (ends("ance")) break;
                        if (ends("ence")) break; return;
                    case 'e':
                        if (ends("er")) break; return;
                    case 'i':
                        if (ends("ic")) break; return;
                    case 'l':
                        if (ends("able")) break;
                        if (ends("ible")) break; return;
                    case 'n':
                        if (ends("ant")) break;
                        if (ends("ement")) break;
                        if (ends("ment")) break;
                        /* element etc. not stripped before the m */
                        if (ends("ent")) break; return;
                    case 'o':
                        if (ends("ion") && j >= 0 && (b[j] == 's' || b[j] == 't')) break;
                        /* j >= 0 fixes Bug 2 */
                        if (ends("ou")) break; return;
                    /* takes care of -ous */
                    case 's':
                        if (ends("ism")) break; return;
                    case 't':
                        if (ends("ate")) break;
                        if (ends("iti")) break; return;
                    case 'u':
                        if (ends("ous")) break; return;
                    case 'v':
                        if (ends("ive")) break; return;
                    case 'z':
                        if (ends("ize")) break; return;
                    default:
                        return;
                }
                if (m() > 1)
                    k = j;
            }

            /* step6() removes a final -e if m() > 1. */
            /// <summary>
            /// Removes final -e if m() > 1
            /// </summary>
            private void step6()
            {
                j = k;

                if (b[k] == 'e')
                {
                    int a = m();
                    if (a > 1 || a == 1 && !cvc(k - 1))
                        k--;
                }
                if (b[k] == 'l' && doublec(k) && m() > 1)
                    k--;
            }

            /** Stem the word placed into the Stemmer buffer through calls to add().
             * Returns true if the stemming process resulted in a word different
             * from the input.  You can retrieve the result with
             * getResultLength()/getResultBuffer() or toString().
             */

            /// <summary>
            /// Stem the word placed into the Stemmer buffer through calls to add()
            /// </summary>
            /// <remarks>
            /// Returns true if the stemming process resulted in a word different from the input.  You can retrieve the result with getResultLength()/getResultBuffer() or toString().
            /// </remarks>
            public void stem()
            {
                k = i - 1;
                if (k > 1)
                {
                    step1();
                    step2();
                    step3();
                    step4();
                    step5();
                    step6();
                }
                i_end = k + 1;
                i = 0;
            }

            /// <summary>
            /// Call to stem the string in the parameter
            /// </summary>
            /// <param name="inputString">String to be stemmed</param>
            public void stem(String inputString)
            {
                foreach (char x in inputString)
                {
                    add(x);
                }
                stem();
            }

            /// <summary>
            /// Stems the subject of an email
            /// </summary>
            /// <param name="inputString">String representation of the email title</param>
            /// <param name="emailID">String representation of the emailID</param>
            public void stemSubject(String inputString, String emailID)
            {
                //Deliminator list for splitting the strings
                List<char> denom = new List<char>();
                //integer for the following for loops
                int i = 0;

                //these for loops input all the characters in the ascii character set
                //that are not number or letters into the deliminator list
                for (i = 32; i <= 47; i++)
                {
                    denom.Add((char)i);
                }
                for (i = 58; i <= 64; i++)
                {
                    denom.Add((char)i);
                }
                for (i = 91; i <= 96; i++)
                {
                    denom.Add((char)i);
                }
                for (i = 123; i <= 126; i++)
                {
                    denom.Add((char)i);
                }

                //add tab to the deliminator list
                denom.Add('\t');

                //convert the list of characters to a character array
                char[] denomintators = denom.ToArray();

                //create a string array of all the individual words in the subject line
                String[] subjectWords = inputString.Split(denomintators);

                //for each string compare it to the stop list and then run porter stemmer on the word
                //then the word will be added to the subject index
                foreach (String words in subjectWords)
                {
                    //if the word is not on the stop list or empty then contune to porter stemmer
                    if ((!Stemmer.StopList.Contains(words))  && (!words.Equals("")))
                    {
                        //ArrayList to hold all email entry IDs associated with mail sent by an email address
                        ArrayList emailIDs = new ArrayList();

                        //run porter stemmer on the word
                        this.stem(words);
                        String wordStem = this.ToString();
                       
                        //if sender email address is NOT already in the index
                        if (InvertedIndexes.SubjectIndex.Contains(wordStem) == false)
                        {
                            //put the emailaddress in a ArrayList
                            emailIDs.Add(emailID);
                            //add sender email address (key) and ArrayList of Email Entry IDs (value) to the index
                            InvertedIndexes.SubjectIndex.Add(wordStem, emailIDs);
                            

                        }
                        else //sender email address is already in the index
                        {
                            //get the ArrayList containg the Email Entry IDs for the key (sender Email address) 
                            emailIDs = (ArrayList)InvertedIndexes.SubjectIndex[wordStem];
                            
                            //add the found Emails ID to the array
                            emailIDs.Add(emailID);
                            
                            //put the array back into the index, overwriting the old one
                            InvertedIndexes.SubjectIndex[wordStem] = emailIDs;
                            

                        }
                        
                    }
                }



            }


        }




    }
