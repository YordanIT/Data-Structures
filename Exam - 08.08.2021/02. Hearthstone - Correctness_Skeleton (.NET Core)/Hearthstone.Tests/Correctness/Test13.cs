﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

public class Test13
{
    [TestCase]
    public void Play_Should_Increase_Attacker_TotalScore_When_Enemy_Killed()
    {
        //Arrange
        var card = new Card("Gnome the grudge", 10, 20, 5);
        var card2 = new Card("Magic Card", 10, 15, 5);
        var card3 = new Card("No magic Card", 6, 8, 3);
        var card4 = new Card("Simple card", 10, 8, 3);

        var board = new Board();

        //Act
        board.Draw(card);
        board.Draw(card2);
        board.Draw(card3);
        board.Draw(card4);

        board.Play("Gnome the grudge", "Magic Card");
        board.Play("Gnome the grudge", "Magic Card");


        //Assert
        Assert.AreEqual(25, card.Score);

    }
}
