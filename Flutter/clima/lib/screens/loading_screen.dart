import 'dart:convert';
import 'package:clima/services/location.dart';
import 'package:clima/services/networking.dart';
import 'package:flutter/material.dart';

const apiKey = '036ad39ce1fbe3dec5660bea79900945';

class LoadingScreen extends StatefulWidget {
  @override
  _LoadingScreenState createState() => _LoadingScreenState();
}

class _LoadingScreenState extends State<LoadingScreen> {
  double latitude;
  double longitude;

  @override
  void initState() {
    super.initState();
    getLocationData();
  }

  void getLocationData() async {
    var location = Location();
    await location.getLocation();
    latitude = location.latitude;
    longitude = location.longitude;
    
    var networkHelper = NetworkHelper('https://api.openweathermap.org/data/2.5/weather?lat=$latitude&lon=$longitude&appid=$apiKey');
    var data = await networkHelper.getData();



  }

  

  @override
  Widget build(BuildContext context) {
    return Scaffold();
  }
}
