/*
	Author: Ethan Skemp
	Date Created: 01 Aug 2017
	Date Last Edited: 23 Aug 2017 by Matthew Baker
	Purpose: Open a KML file exported from Google Maps Pro as a CSV and enable editting in CSV format to take effect in KML document
*/


#include <iostream>
#include <cstdlib>
#include <cmath>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>

#include "xml_reader.h"
#include "xml_remover.h"

#define CSV_output_name "csvout.kmlcsv"
#define CSV_input_name "UpdatedCSV.kmlcsv"
//#define in_out_mode 1 // 0 for out, 1 for in


using namespace std;

int main(int argc, char * argv[]) {

	for (int i = 0; i < 4; i++) {
		cout << argv[i] << endl;
	}

	bool in_out_mode = (int) *argv[2]-48;
	// need method to determine in/out functionality 
	// need method to determine headers of interest (unique_header_index)
	// need method to recieve/get	original KML file
	//								CSV file
	//								new KML file name

	string file_name = argv[3];
	string new_KML_name = argv[4];
	
	fstream KML_fh;
	fstream CSV_fh;                               
	fstream new_KML_fh;
	vector<string> headers;
	vector<int> unique_header_index;

	// this section needs to be updated based on the order of the information
	// would be good to have user input (select which columns, etc)
	// perhaps read from a separately created text file from VBA/C#
	// 0 index. i.e., 0 - A, 1 - B, 2 - C, etc
	unique_header_index.push_back(0);
	unique_header_index.push_back(2);
	unique_header_index.push_back(3);

	sort(unique_header_index.begin(), unique_header_index.end());

	if (in_out_mode == 0) {

		KML_fh.open(file_name, fstream::in);
		CSV_fh.open(CSV_output_name, fstream::out);

		//leave_header_info(KML_fh);
		headers = create_CSV_headers(CSV_fh, KML_fh);
		create_CSV_entries(CSV_fh, KML_fh, headers);
	}
	else if (in_out_mode == 1) {

		CSV_fh.open(CSV_input_name, fstream::in);
		KML_fh.open(file_name, fstream::in);
		new_KML_fh.open(new_KML_name, fstream::out);

		count_CSV_records(CSV_fh);
		headers = get_header_list(CSV_fh);
		strip_KML(CSV_fh, new_KML_fh, KML_fh, unique_header_index);

		new_KML_fh.close();
	}
	
	KML_fh.close();
	CSV_fh.close();

}