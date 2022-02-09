import gensim
import re
import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
import string
from nltk.stem.isri import ISRIStemmer
from keras.models import load_model

class TextEmotion:

    dropout_rate = 0.2
    _dropout_rate_softmax = 0.5
    _number_of_inputs = 140 #max number of words /characters per doc(tweet)
    _vector_size = 300 #vector for each word
    _batch_size = 100
    _kernal_size= 5 #An integer or tuple/list of a single integer
    _pool_size = 3
    _noise_shape = (_batch_size,1,_number_of_inputs)
    _epochs = 25
    _test_size = 0.33 # percentage of test from the dataset
    _Learning_rate = 0.0001

    _aravec_model_name = "full_grams_sg_300_twitter" 
    _emotional_modle_name = "trail_rev.h5"


    my_api_key = "AIzaSyCUKEOsT6ecC3ods862vgsVOawWyii0NDQ"
    my_cse_id = "007967891901694126580:i3iq-cjlldq"

        #مسح التشكيل و علامات الترقيم و الحروف المتكررة---------
    arabic_punctuations = '''`÷×؛<>_()*&^%][ـ،/:"؟.,'{}~¦+|!”…“–ـ»«•'''
    english_punctuations = string.punctuation
    english_numbers = "0123456789"
    punctuations_list = arabic_punctuations + english_punctuations + english_numbers
    arabic_diacritics = re.compile("""
                ّ    | # Tashdid
                َ    | # Fatha
                ً    | # Tanwin Fath
                ُ    | # Damma
                ٌ    | # Tanwin Damm
                ِ    | # Kasra
                ٍ    | # Tanwin Kasr
                ْ    | # Sukun
            """, re.VERBOSE)




    def __init__(self) -> None:
        
        self.t_model= gensim.models.Word2Vec.load(TextEmotion._aravec_model_name +'.mdl')
        self.emotional_model = load_model(TextEmotion._emotional_modle_name)

    

    @staticmethod
    def google_search(search_term):
        service = build("customsearch", "v1", developerKey=my_api_key)
        res = service.cse().list(q=search_term, cx=my_cse_id).execute()
        return res['spelling']['correctedQuery']



    @staticmethod
    def normalize_arabic(text):
        text = re.sub("إ", "ا", text)
        text = re.sub("أ", "ا", text)
        text = re.sub("آ", "ا", text)
        text = re.sub("ا", "ا", text)
        text = re.sub("ى", "ي", text)
        text = re.sub("ؤ", "ء", text)
        text = re.sub("ئ", "ء", text)
        text = re.sub("ة", "ه", text)
        text = re.sub("گ", "ك", text)
        return text

    @staticmethod
    def remove_diacritics(text):
        text = re.sub(TextEmotion.arabic_diacritics, '', text)
        return text

    @staticmethod
    def remove_punctuations(text):
        translator = str.maketrans('', '',TextEmotion.punctuations_list)
        return text.translate(translator)

    @staticmethod
    def tokens_remove_stopwords(text):

        text = text.split()
        result = list()
        ch = 0

        arabic_stop_words = ["من", "فى", "الي", "علي", "عن", "حتي", "مذ", "منذ", "و", "الا", "او", "ام", "ثم", "بل", "لكن",
                            "كل" , "متى" , "يوم"]

        for word in text:
            for stop_word in arabic_stop_words:
                if word == stop_word:
                    ch = 1
                    break
            if ch != 1:
                result.append(word)

            ch = 0

        return 
    
    @staticmethod
    def remove_repeating_char(text):
        return re.sub(r'(.)\1+', r'\1', text)

    #_______________________________________
    @staticmethod
    #Rooting words
    def rooting(text):
        result = list()
        for word in text:
            stemmer = ISRIStemmer()
            result.append(stemmer.stem(word))
        return result

    @staticmethod
    #remove english and empty strings
    def remove_english(tokens):
        filtered_tokens = list()
        for word in tokens:
            if (not re.match(r'[a-zA-Z]+', word, re.I)) and word != '':
                filtered_tokens.append(word)
        return filtered_tokens

    @staticmethod
    def preprocess1(text):
        text = str(text)
        text = TextEmotion.remove_diacritics(text)
        text = TextEmotion.remove_punctuations(text)
        text = TextEmotion.normalize_arabic(text)
        text = TextEmotion.remove_repeating_char(text)
        tokens = re.split(" ", text)
        tokens = TextEmotion.remove_english(tokens)
        return tokens

    @staticmethod
    def preprocess2(text):
        text = str(text)
        text = TextEmotion.remove_diacritics(text)
        text = TextEmotion.remove_punctuations(text)
        text = TextEmotion.normalize_arabic(text)
        text = TextEmotion.remove_repeating_char(text)
        text = TextEmotion.tokens_remove_stopwords(text)
        text = TextEmotion.remove_english(text)
        text = TextEmotion.rooting(text)
        return text

    
    def embed_doc(self,text):
        preprocessed_text = TextEmotion.preprocess1(text)
        #print(preprocessed_text)
        
        embedded_vectors = np.zeros(shape=(TextEmotion._number_of_inputs,TextEmotion._vector_size))#np array of arrays (array of 100/300 float number per word)
        embedded_vectors_index = 0
        for i in range(len(preprocessed_text)):
            try:
                embedded_vectors[embedded_vectors_index] = self.t_model.wv[preprocessed_text[i]]
                embedded_vectors_index = embedded_vectors_index + 1
            except:
                try:
                    result = TextEmotion.rooting([preprocessed_text[i]])[0]
                    embedded_vectors[embedded_vectors_index] = self.t_model.wv[result]
                    embedded_vectors_index = embedded_vectors_index + 1
                except:
                    try:
                        search_output = TextEmotion.google_search(preprocessed_text[i])
                        tokens = re.split(" ", search_output)
                        for j in range(len(tokens)):
                            try:
                                embedded_vectors[embedded_vectors_index] = self.t_model.wv[tokens[j]]
                                embedded_vectors_index = embedded_vectors_index + 1
                            except:
                                pass #print(tokens[j] + " Sub word cant be embedded")
                    except:
                        pass # print(preprocessed_text[i] + "word cant be embedded") #currently emojis can't be embedded and for any extreme case (skip wrongly written words)
        return embedded_vectors

    @staticmethod
    def embed_label(label):
        if label == "anger":
            return 0
        if label == "joy":
            return 1
        if label == "none":
            return 2
        if label == "surprise":
            return 3
        if label == "sadness":
            return 4
        if label == "fear":
            return 5
        if label == "sympathy":
            return 6
        if label == "love":
            return 7
    
    @staticmethod
    def translate_label(label):
        if label == 0:
            return  "غضب"
        if label == 1:
            return "فرح"
        if label == 2:
            return "طبيعي"
        if label == 3:
            return "متفاجئ"
        if label == 4:
            return "حزن"
        if label == 5:
            return "خوف"
        if label == 6:
            return "تعاطف"
        if label == 7:
            return "حب"

    
    def Predict(self,sentence):
        doc = sentence

        embedded_vector = self.embed_doc(doc)
        shape = np.shape(embedded_vector)
        embedded_vector = np.array(embedded_vector).reshape(1,shape[0],shape[1])
        #label = emotional_model.predict_classes(embedded_vector)
        predict_x = self.emotional_model.predict(embedded_vector) 
        classes_x = np.argmax(predict_x,axis=1)
        textlabel = TextEmotion.translate_label(classes_x)
        print ("الحالة:",textlabel)
    