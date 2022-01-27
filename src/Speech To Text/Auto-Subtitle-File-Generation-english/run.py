"""Take user input and run the program.

This should be the Only entry for the program. This project
uses absolute import system, so moving this script to other place
will cause System Error. Just go in the project root folder and run
this script
"""

from generator import generator
from config import STTInferenceConfig
import os

if __name__ == '__main__':

    # Choose the target file path to generate subtitles.
    target_file_name = r"./Video/o2.mp4"

    # Choose your output file name.
    output_file_name = 'subtitle.srt'

    # Choose the lanauge.
    choices = {}
    for (i, (lang, lang_name)) in enumerate(STTInferenceConfig().iter_avl_langs()):

        choices[str(i)] = lang
    language_idx = "0"

    # If source_language is empty, STT won't work (only timelines will be output).
    source_language = choices[language_idx] if language_idx.strip() else ""

    generator(targ=target_file_name,
              fname=output_file_name, lang=source_language)

    print("The output subtitle file is under result")
