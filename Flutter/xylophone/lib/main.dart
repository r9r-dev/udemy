import 'package:flutter/material.dart';
import 'package:audioplayers/audio_cache.dart';

void main() => runApp(XylophoneApp());

class XylophoneApp extends StatelessWidget {
  void playSound(int note) {
    final player = AudioCache();
    player.play('note$note.wav');
  }

  Expanded buildKey({int sound, MaterialColor color}) => Expanded(
        child: FlatButton(
          color: color,
          onPressed: () {
            playSound(sound);
          },
        ),
      );

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        backgroundColor: Colors.black,
        body: SafeArea(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              buildKey(sound: 1, color: Colors.red),
              buildKey(sound: 2, color: Colors.orange),
              buildKey(sound: 3, color: Colors.yellow),
              buildKey(sound: 4, color: Colors.red),
              buildKey(sound: 5, color: Colors.blue),
              buildKey(sound: 6, color: Colors.teal),
              buildKey(sound: 7, color: Colors.purple),
            ],
          ),
        ),
      ),
    );
  }
}
