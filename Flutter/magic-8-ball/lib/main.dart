import 'dart:math';

import 'package:flutter/material.dart';

void main() => runApp(
      MaterialApp(
        home: Scaffold(
          backgroundColor: Colors.blue,
          appBar: AppBar(
            title: Text('Ask Me Anything'),
            backgroundColor: Colors.blue.shade900,
          ),
          body: Ball(),
        ),
      ),
    );

class Ball extends StatefulWidget {
  @override
  _BallState createState() => _BallState();
}

class _BallState extends State<Ball> {
  var ballNumber = 1;
  @override
  Widget build(BuildContext context) {
    return Center(
        child: FlatButton(
          onPressed: () {
            changeBall();
          },
          child: Image.asset('images/ball$ballNumber.png'),
        ),      
    );
  }

  void changeBall() {
  return setState(() {
    ballNumber = Random().nextInt(5) + 1;
  });
}
}


