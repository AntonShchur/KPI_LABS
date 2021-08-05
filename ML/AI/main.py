import cv2
import mediapipe as mp
import keyboard as kb
import time
from sound import Sound
import numpy as np


sound = Sound()


cap = cv2.VideoCapture(0)


mpHands = mp.solutions.hands
hands = mpHands.Hands()
mpDraw = mp.solutions.drawing_utils

while True:
    success, img = cap.read()

    imgRGB = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    results = hands.process(imgRGB)

    if results.multi_hand_landmarks:
        index_tip_1 = results.multi_hand_landmarks[0].landmark[mpHands.HandLandmark.INDEX_FINGER_TIP].x
        index_tip_2 = results.multi_hand_landmarks[len(results.multi_hand_landmarks) -1].landmark[mpHands.HandLandmark.INDEX_FINGER_TIP].x
        print(f'{index_tip_1} - {index_tip_2}')
        mpDraw.draw_landmarks(img, results.multi_hand_landmarks[0], mpHands.HAND_CONNECTIONS)
        mpDraw.draw_landmarks(img, results.multi_hand_landmarks[len(results.multi_hand_landmarks) -1], mpHands.HAND_CONNECTIONS)
        value = np.abs(index_tip_1 - index_tip_2) * 15 ** 2
        sound.volume_set(value)


    cv2.imshow("228", img)
    cv2.waitKey(1)

