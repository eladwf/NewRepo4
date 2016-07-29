using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GeekQuiz.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<TriviaDbContext>();
            context.Database.Migrate();

            if (!context.TriviaQuestions.Any())
            {
                var questions = new List<TriviaQuestion>();

                questions.Add(new TriviaQuestion
                {
                    Title = "A 1998 study suggests that which of the following explorers reached the North Pole?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Roald Amundsen", IsCorrect= false },
                        new TriviaOption { Title= "William Barents", IsCorrect= false },
                        new TriviaOption { Title= "Robert E. Peary", IsCorrect= true },
                        new TriviaOption { Title= "Adam Sound", IsCorrect= false }

                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "History students are taught about the 'the fall of Constantinople' in 1453. to who did it fall?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Christian crusaders", IsCorrect= false },
                        new TriviaOption { Title= "Mongol hordes", IsCorrect= false },
                        new TriviaOption { Title= "Romans", IsCorrect= false },
                        new TriviaOption { Title= "Ottoman Turks", IsCorrect= true }
                    }).ToList()
                    ,Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Catherine the Great ruled what country?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "England", IsCorrect= false },
                        new TriviaOption { Title= "France", IsCorrect= false },
                        new TriviaOption { Title= "Russia", IsCorrect= true },
                        new TriviaOption { Title= "Germany", IsCorrect= false }
                    }).ToList()
                    ,
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "What was the next state after the original 13 to be admitted to the United States?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Kentucky", IsCorrect= false },
                        new TriviaOption { Title= "West Virginia", IsCorrect= false },
                        new TriviaOption { Title= "Vermont", IsCorrect= true },
                        new TriviaOption { Title= "Florida", IsCorrect= false }
                    }).ToList()
                    ,
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "In what year was the first Voice Over IP (VOIP) call made?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "1973", IsCorrect= true },
                        new TriviaOption { Title= "1982", IsCorrect= false },
                        new TriviaOption { Title= "1991", IsCorrect= false },
                        new TriviaOption { Title= "1994", IsCorrect= false }
                    }).ToList()
                    ,
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Which of these four wise men died before the other three were born?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Plato", IsCorrect= false },
                        new TriviaOption { Title= "Confucius", IsCorrect= false },
                        new TriviaOption { Title= "Solomon", IsCorrect= true },
                        new TriviaOption { Title= "Jesus", IsCorrect= false }
                    }).ToList()
                    ,
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "The Creoles of today's New Orleans are descended from whom?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "French Canadians from Nova Scotia", IsCorrect= false },
                        new TriviaOption { Title= "refugees from the Crimean War", IsCorrect= false },
                        new TriviaOption { Title= "French and Spanish settlers who lived in the city", IsCorrect= true },
                        new TriviaOption { Title= "none of the above", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "George Washington is called the father of the US, but how many kids did Mr. Washington really have?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "1", IsCorrect= false },
                        new TriviaOption { Title= "0", IsCorrect= true },
                        new TriviaOption { Title= "3", IsCorrect= false },
                        new TriviaOption { Title= "7", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "William Rehnquist as chief justice of the Supreme Court was preceded by whom?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Warren Burger", IsCorrect= true },
                        new TriviaOption { Title= "Abe Fortas", IsCorrect= false },
                        new TriviaOption { Title= "Earl Warren", IsCorrect= false },
                        new TriviaOption { Title= "Clarence Thomas", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "What were the first names of the famous explorers Lewis and Clark?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Meriwether and William", IsCorrect= true },
                        new TriviaOption { Title= "Benjamin and Samuel", IsCorrect= false },
                        new TriviaOption { Title= "Clark and Lewis", IsCorrect= false },
                        new TriviaOption { Title= "John and Lincoln", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Manuel Noriega took refuge in whose embassy after the U.S. invasion of Panama City in 1989?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Vatican City", IsCorrect= true },
                        new TriviaOption { Title= "Cuba", IsCorrect= false },
                        new TriviaOption { Title= "Switzerland", IsCorrect= false },
                        new TriviaOption { Title= "Nicaragua", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "On the island nation formerly known as Ceylon, Tamil separatists have been conducting attacks against the Sinhalese majority. what is the name of the country?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Cyprus", IsCorrect= false },
                        new TriviaOption { Title= "Seychelles", IsCorrect= false },
                        new TriviaOption { Title= "Madagascar", IsCorrect= false },
                        new TriviaOption { Title= "Sri Lanka", IsCorrect= true }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Where was the UN headquarters located prior to them moving to Manhattan's East Side?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "San Francisco", IsCorrect= false },
                        new TriviaOption { Title= "Geneva, Switzerland", IsCorrect= false },
                        new TriviaOption { Title= "Long Island, New York", IsCorrect= true },
                        new TriviaOption { Title= "Paris, France", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Which American commander said, 'I have not yet begun to fight'?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "David Farragut", IsCorrect= false },
                        new TriviaOption { Title= "George Dewey", IsCorrect= false },
                        new TriviaOption { Title= "John Paul Jones", IsCorrect= true },
                        new TriviaOption { Title= "Oliver Hazard Perry", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "John Brown of Civil War fame was which?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "an abolitionist", IsCorrect= true },
                        new TriviaOption { Title= "a slave", IsCorrect= false },
                        new TriviaOption { Title= "a slave-owner", IsCorrect= false },
                        new TriviaOption { Title= "none of the above", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "When was the internet opened to commercial use?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "1989", IsCorrect= false },
                        new TriviaOption { Title= "1992", IsCorrect= false },
                        new TriviaOption { Title= "1990", IsCorrect= false },
                        new TriviaOption { Title= "1991", IsCorrect= true }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "At the time of the Declaration of Independence what was the approximate  population of the United States?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "20,000", IsCorrect= false },
                        new TriviaOption { Title= "2,000,000", IsCorrect= true },
                        new TriviaOption { Title= "200,000", IsCorrect= false },
                        new TriviaOption { Title= "20,000,000", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "What frontier marshal was murdered in 1876 in Deadwood, South Dakota, by outlaw Jack McCall?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Wild Bill Hickok", IsCorrect= true},
                        new TriviaOption { Title= "Matt Dillon", IsCorrect= false },
                        new TriviaOption { Title= "Buffalo Bill Cody", IsCorrect= false },
                        new TriviaOption { Title= "Bat Masterson", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "In what year did Wyatt Earp die?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "1879", IsCorrect= false },
                        new TriviaOption { Title= "1889", IsCorrect= false },
                        new TriviaOption { Title= "1909", IsCorrect= false },
                        new TriviaOption { Title= "1929", IsCorrect= true }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "In what year did Leonard Kristensen of Norway lead the first party to land on the mainland of Antarctica?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "1795", IsCorrect= false },
                        new TriviaOption { Title= "1845", IsCorrect= false },
                        new TriviaOption { Title= "1945", IsCorrect= false },
                        new TriviaOption { Title= "1895", IsCorrect= true }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "When was the first laser mouse released?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "2001", IsCorrect= false },
                        new TriviaOption { Title= "2002", IsCorrect= false },
                        new TriviaOption { Title= "2003", IsCorrect= false },
                        new TriviaOption { Title= "2004", IsCorrect= true }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Three of the four following events took place in 1985, which one happened in 1982?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Mikhail Gorbachev became the leader of the Soviet Union", IsCorrect= false },
                        new TriviaOption { Title= "Britain and Argentina fought a war over the Falkland Islands.", IsCorrect= true },
                        new TriviaOption { Title= "Coca Cola replaced its formula to create a 'new' Coke.", IsCorrect= false },
                        new TriviaOption { Title= "Pete Rose broke Ty Cobb's record of 4,191 career hits.", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Namibia became a colony of what European nation in 1890, under the name South-West Africa?",
                    Options = (new TriviaOption[]
                    {
                        new TriviaOption { Title= "Great Britain", IsCorrect= false },
                        new TriviaOption { Title= "Germany", IsCorrect= true },
                        new TriviaOption { Title= "The Netherlands", IsCorrect= false },
                        new TriviaOption { Title= "Portugal", IsCorrect= false }
                    }).ToList(),
                    Cat = Room.Catrgory.History
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Which 19th century poet and novelist, when age twenty-six, married his thirteen year old cousin?",
                    Options = (new TriviaOption[]
    {
                        new TriviaOption { Title= "Victor Hugo", IsCorrect= false },
                        new TriviaOption { Title= "Edgar Allan Poe", IsCorrect= true },
                        new TriviaOption { Title= "Oscar Wilde", IsCorrect= false },
                        new TriviaOption { Title= "none of the above", IsCorrect= false }
    }).ToList(),
                    Cat = Room.Catrgory.General
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Samite is a type of what?",
                    Options = (new TriviaOption[]
{
                        new TriviaOption { Title= "Stone", IsCorrect= false },
                        new TriviaOption { Title= "Fabric", IsCorrect= true },
                        new TriviaOption { Title= "Cake", IsCorrect= false },
                        new TriviaOption { Title= "none of the above", IsCorrect= false }
}).ToList(),
                    Cat = Room.Catrgory.General
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Vermillion is a shade of which colour?",
                    Options = (new TriviaOption[]
{
                        new TriviaOption { Title= "Green", IsCorrect= false },
                        new TriviaOption { Title= "Red", IsCorrect= true },
                        new TriviaOption { Title= "Blue", IsCorrect= false },
                        new TriviaOption { Title= "Yellow", IsCorrect= false }
}).ToList(),
                    Cat = Room.Catrgory.General
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "In the 1991 film Reservoir Dogs, Michael Madson played which character?",
                    Options = (new TriviaOption[]
{
                        new TriviaOption { Title= "Mr Orange", IsCorrect= false },
                        new TriviaOption { Title= "Mr Blonde", IsCorrect= true },
                        new TriviaOption { Title= "Mr Brown", IsCorrect= false },
                        new TriviaOption { Title= "none of the above", IsCorrect= false }
}).ToList(),
                    Cat = Room.Catrgory.General
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "In the 1991 film Reservoir Dogs, Michael Madson played which character?",
                    Options = (new TriviaOption[]
                {
                        new TriviaOption { Title= "Mr Orange", IsCorrect= false },
                        new TriviaOption { Title= "Mr Blonde", IsCorrect= true },
                        new TriviaOption { Title= "Mr Brown", IsCorrect= false },
                        new TriviaOption { Title= "none of the above", IsCorrect= false }
                }).ToList(),
                    Cat = Room.Catrgory.General
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "Evo Morales became president of which country in 2006?",
                    Options = (new TriviaOption[]
                {
                        new TriviaOption { Title= "Argentina", IsCorrect= false },
                        new TriviaOption { Title= "Bolivia", IsCorrect= true },
                        new TriviaOption { Title= "Ecuador", IsCorrect= false },
                        new TriviaOption { Title= "Peru", IsCorrect= false }
                }).ToList(),
                    Cat = Room.Catrgory.General
                });


                questions.Add(new TriviaQuestion
                {
                    Title = "The port of Mocha is in which country?",
                    Options = (new TriviaOption[]
            {
                        new TriviaOption { Title= "Somalia", IsCorrect= false },
                        new TriviaOption { Title= "Yemen", IsCorrect= true },
                        new TriviaOption { Title= "Iran", IsCorrect= false },
                        new TriviaOption { Title= "Oman", IsCorrect= false }
            }).ToList(),
                    Cat = Room.Catrgory.General
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "A nide is a brood or nest of which type of birds?",
                    Options = (new TriviaOption[]
{
                        new TriviaOption { Title= "Emus", IsCorrect= false },
                        new TriviaOption { Title= "Pheasants", IsCorrect= true },
                        new TriviaOption { Title= "Sparrows", IsCorrect= false },
                        new TriviaOption { Title= "Swans", IsCorrect= false }
}).ToList(),
                    Cat = Room.Catrgory.General
                });

                questions.Add(new TriviaQuestion
                {
                    Title = "An anemometer is a guage used for recording the speed of what?",
                    Options = (new TriviaOption[]
{
                        new TriviaOption { Title= "Light", IsCorrect= false },
                        new TriviaOption { Title= "Wind", IsCorrect= true },
                        new TriviaOption { Title= "Spacecraft", IsCorrect= false },
                        new TriviaOption { Title= "Athletes", IsCorrect= false }
}).ToList(),
                    Cat = Room.Catrgory.General
                });

                context.TriviaQuestions.AddRange(questions);

                context.SaveChanges();
            }
        }
    }
}

